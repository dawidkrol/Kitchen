using Kitchen.App.Data;
using Kitchen.App.Models;
using Kitchen.Library.Data;
using Kitchen.Library.DataModels;
using Kitchen.Library.DbAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kitchen.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                    connectionString: Configuration.GetConnectionString("Auth")
                    ));

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Auth.Cookie";
                config.LoginPath = "/Home/Authenticate";
            });

            services.AddAutoMapper(config =>
            {
                config.CreateMap<PrzepisViewModel, PrzepisData>();
                config.CreateMap<PrzepisData, PrzepisViewModel>();

                config.CreateMap<PrzepisDetailsViewModel, PrzepisData>();
                config.CreateMap<PrzepisData, PrzepisDetailsViewModel>();
            });

            services.AddControllersWithViews();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddScoped<IMasterChefData, MasterChefData>();
            services.AddScoped<IPrzepisyData, PrzepisyData>();
            services.AddScoped<ICategoryStructData, CategoryStructData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Who are you?
            app.UseAuthentication();

            //Are you allowed?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
