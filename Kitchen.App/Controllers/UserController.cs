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
    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IAuthorData _authorData;

        public UserController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<UserController> logger,
            IAuthorData authorData)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _authorData = authorData;
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

            return RedirectToAction("Index", "Home");
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

                await _authorData.AddAuthorAsync(addedUser.Id, register.Name, register.Surname, register.Email, register.PhoneNumber);

                //TODO: Create decorator for Identityuser with better ToString() method;
                _logger.LogInformation("User [\n\t{\n\t\tID: {userId};\n\t\tUserName: {username};\n\t}\n]\n has been created correctly", addedUser.Email, addedUser.UserName);
            }
            else
            {
                return BadRequest("Cannot create this account");
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            _logger.LogInformation("User {user} is logged out", HttpContext.User.Claims.Where(x => x.Type.Contains("emailaddress")).Single().Value);
            Response.Cookies.Delete("Auth.Cookie");
            return RedirectToAction("Index", "Home");
        }
    }
}
