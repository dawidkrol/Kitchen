using Kitchen.App.Models;
using Kitchen.Library.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kitchen.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAuthorData _masterChef;
        private readonly ICategoryStructData _structData;

        public HomeController(ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IAuthorData masterChef,
            ICategoryStructData structData)
        {
            _signInManager = signInManager;
            _masterChef = masterChef;
            _structData = structData;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _structData.GetCategories();
                _logger.LogInformation("Loadning home page");
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Cannot load index page");
            }
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
                await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                _logger.LogInformation("User {user} is logged in", user.Email);
            }
            else
            {
                _logger.LogError("Cannot find user {user}", login.Email);
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

            if (addedUser != null)
            {
                _logger.LogError("User with email {email} already exists.", register.Email);
                return BadRequest("User already exists");
            }

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                addedUser = await _userManager.FindByEmailAsync(register.Email);

                await _masterChef.AddAuthorAsync(addedUser.Id, register.Name, register.Surname, register.Email, register.PhoneNumber);

                //TODO: Create decorator for Identityuser with better ToString() method;
                _logger.LogInformation("User [\n\t{\n\t\tID: {userId};\n\t\tUserName: {username};\n\t}\n]\n has been created correctly", addedUser.Email, addedUser.UserName);
            }
            else
            {
                return BadRequest("Cannot create this account");
            }

            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            _logger.LogInformation("User {user} is logged out", HttpContext.User.Claims.Where(x => x.Type.Contains("emailaddress")).Single().Value);
            Response.Cookies.Delete("Auth.Cookie");
            return RedirectToAction("Index");
        }
    }
}