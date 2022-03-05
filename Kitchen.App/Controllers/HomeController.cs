using Kitchen.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Kitchen.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);

            if (user != null)
            {
                //sign in
                await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel login)
        {
            var user = new IdentityUser
            {
                UserName = login.Username
            };
            var result = await _userManager.CreateAsync(user, login.Password);
            //---------------------------------------------------------------
            //if (await _roleManager.FindByNameAsync("Admin") == null)
            //{
            //    //Add role
            //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
            //}
            if (result.Succeeded)
            {
                ////Add to role
                ////await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(username), "Admin");
                ////sign user here
                //await _signInManager.PasswordSignInAsync(user, password, false, false);

                //return RedirectToAction("EmailVerification");
            }
            return RedirectToAction("Index");
        }
        public IActionResult EmailVerification() => View();
    }
}
