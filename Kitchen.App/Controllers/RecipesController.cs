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
        private readonly IRecipesData _recipesData;
        private readonly IMapper _mapper;
        private readonly ICategoryStructData _categoryStructData;
        private readonly IOriginData _originData;

        public RecipesController(
            IRecipesData przepisyData,
            IMapper mapper,
            ICategoryStructData categoryStructData,
            IOriginData pochodzenieData)
        {
            _recipesData = przepisyData;
            _mapper = mapper;
            _categoryStructData = categoryStructData;
            _originData = pochodzenieData;
        }
        [HttpGet("Recipes/index/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            IEnumerable<RecipeData> _data = await _recipesData.Get(id);
            return View(_mapper.Map<IEnumerable<RecipeViewModel>>(_data));
        }
        public async Task<IActionResult> Details(int id)
        {
            var det = await _recipesData.GetById(id);
            return View(_mapper.Map<RecipeDetailsViewModel>(det));
        }
        [Authorize]
        [HttpGet("/Recipes/add")]
        public async Task<IActionResult> Add()
        {
            var a = await _categoryStructData.GetCategories();
            ViewData["Categories"] = a;
            ViewData["Regions"] = _mapper.Map<IEnumerable<RegionViewModel>>(await _originData.Get());

            return View();
        }
        [Authorize]
        [HttpPost("/Recipes/add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RecipeDetailsViewModel model, IFormCollection collection)
        {
            model.UserId = HttpContext.User.Claims.Where(x => x.Type.Contains("nameidentifier")).Single().Value;

            await _recipesData.Add(_mapper.Map<RecipeData>(model));
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetMy()
        {
            var userId = HttpContext.User.Claims.Where(x => x.Type.Contains("nameidentifier")).Single().Value;
            var data = await _recipesData.GetByUserId(userId);
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
            await _recipesData.Delete(id);
            return RedirectToAction("Index","Home");
        }
    }
}
