using Kitchen.Library.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kitchen.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryStructData _structData;

        public HomeController(ILogger<HomeController> logger,
            ICategoryStructData structData)
        {
            _structData = structData;
            _logger = logger;
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
    }
}