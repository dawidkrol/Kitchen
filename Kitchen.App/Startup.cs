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
using Serilog;

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

            services.AddIdentity<IdentityUserModel, IdentityRole>(config =>
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

            services.AddLogging();

            services.AddAutoMapper(config =>
            {
                config.CreateMap<RecipeViewModel, RecipeData>();
                config.CreateMap<RecipeData, RecipeViewModel>();

                config.CreateMap<RecipeDetailsViewModel, RecipeData>();
                config.CreateMap<RecipeData, RecipeDetailsViewModel>();

                config.CreateMap<RegionDataModel, RegionViewModel>();
            });

            services.AddControllersWithViews();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddScoped<IAuthorData, AuthorData>();
            services.AddScoped<IRecipesData, RecipesData>();
            services.AddScoped<ICategoryStructData, CategoryStructData>();
            services.AddScoped<IOriginData, OriginData>();
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

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
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

            app.UseSerilogRequestLogging();
        }
    }
}
