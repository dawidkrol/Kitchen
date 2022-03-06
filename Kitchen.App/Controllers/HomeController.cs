using Kitchen.App.Models;
using Kitchen.Library.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kitchen.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMasterChefData _masterChef;

        public HomeController(ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMasterChefData masterChef)
        {
            _signInManager = signInManager;
            _masterChef = masterChef;
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
        public async Task<IActionResult> Login(LogInViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

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
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            var username = $"{register.Name.ToLowerInvariant()}{register.Surname.ToLowerInvariant()}{new Random().Next()}";
            var user = new IdentityUser
            {
                Email = register.Email,
                UserName = new Regex(@"([^a-z])").Replace(username, "0")
            };
            var addedUser = await _userManager.FindByEmailAsync(register.Email);

            if(addedUser != null)
            {
                return BadRequest("User already exists");
            }

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                addedUser = await _userManager.FindByEmailAsync(register.Email);

                await _masterChef.AddMasterChefAsync(addedUser.Id,register.Name, register.Surname, register.Email, register.PhoneNumber);
            }
            return RedirectToAction("Index");
        }
        public IActionResult EmailVerification() => View();
    }
}
