using AutoMapper;
using Kitchen.App.Models;
using Kitchen.Library.Data;
using Kitchen.Library.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.App.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesData _przepisyData;
        private readonly IMapper _mapper;
        private readonly ICategoryStructData _categoryStructData;
        private readonly IOriginData _pochodzenieData;

        public RecipesController(
            IRecipesData przepisyData,
            IMapper mapper,
            ICategoryStructData categoryStructData,
            IOriginData pochodzenieData)
        {
            _przepisyData = przepisyData;
            _mapper = mapper;
            _categoryStructData = categoryStructData;
            _pochodzenieData = pochodzenieData;
        }
        [HttpGet("Przepisy/index/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            IEnumerable<RecipeData> _data = await _przepisyData.Get(id);
            return View(_mapper.Map<IEnumerable<RecipeViewModel>>(_data));
        }
        public async Task<IActionResult> Details(int id)
        {
            var det = await _przepisyData.GetById(id);
            return View(_mapper.Map<RecipeDetailsViewModel>(det));
        }
        [Authorize]
        [HttpGet("/przepisy/add")]
        public async Task<IActionResult> Add()
        {
            ViewData["Categories"] = await _categoryStructData.GetCategories();
            ViewData["Regions"] = _mapper.Map<IEnumerable<RegionViewModel>>(await _pochodzenieData.Get());

            return View();
        }
        [Authorize]
        [HttpPost("/przepisy/add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RecipeDetailsViewModel model, IFormCollection collection)
        {
            model.UserId = HttpContext.User.Claims.Where(x => x.Type.Contains("nameidentifier")).Single().Value;

            await _przepisyData.Add(_mapper.Map<RecipeData>(model));
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetMy()
        {
            var userId = HttpContext.User.Claims.Where(x => x.Type.Contains("nameidentifier")).Single().Value;
            var data = await _przepisyData.GetByUserId(userId);
            return View(_mapper.Map<IEnumerable<RecipeViewModel>>(data));
        }
        public async Task<IActionResult> Delete(int id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            await _przepisyData.Delete(id);
            return RedirectToAction("Index","Home");
        }
    }
}
