using AutoMapper;
using Kitchen.App.Models;
using Kitchen.Library.Data;
using Kitchen.Library.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(
            IRecipesData przepisyData,
            IMapper mapper,
            ICategoryStructData categoryStructData,
            IOriginData pochodzenieData,
            ILogger<RecipesController> logger)
        {
            _recipesData = przepisyData;
            _mapper = mapper;
            _categoryStructData = categoryStructData;
            _originData = pochodzenieData;
            _logger = logger;
        }
        [HttpGet("Recipes/index/{categoryId}")]
        public async Task<IActionResult> Index(int categoryId)
        {
            try
            {
                _logger.LogInformation("Loading recipes with category id: {id}", categoryId);
                IEnumerable<RecipeData> _data = await _recipesData.Get(categoryId);
                var recipes = _mapper.Map<IEnumerable<RecipeViewModel>>(_data);
                return View(recipes);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error");
            }
       
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                _logger.LogInformation("loading details for redipe with Id: {id}", id);
                var detailsData = await _recipesData.GetById(id);
                var recipeDetails = _mapper.Map<RecipeDetailsViewModel>(detailsData);
                return View(recipeDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error");
            }
        }
        [Authorize]
        [HttpGet("/Recipes/add")]
        public async Task<IActionResult> Add()
        {
            try
            {
                var a = await _categoryStructData.GetCategories();
                ViewData["Categories"] = a;
                ViewData["Regions"] = _mapper.Map<IEnumerable<RegionViewModel>>(await _originData.Get());

                return View();
            }
            //catch (SqlException ex)
            //{
            //    _logger.LogError(ex.Message);
            //    return BadRequest("An error occurred while fetching categories and regions.");
            //}
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error");
            }
        }
        [Authorize]
        [HttpPost("/Recipes/add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RecipeDetailsViewModel model, IFormCollection collection)
        {
            try
            {
                _logger.LogInformation("Adding {title} recipe into database.", model.Title);
                model.UserId = HttpContext.User.Claims.Where(x => x.Type.Contains("nameidentifier")).Single().Value;

                await _recipesData.Add(_mapper.Map<RecipeData>(model));
                _logger.LogInformation("Recipe {title} has been added properly.", model.Title);
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error");
            }
        }

        public async Task<IActionResult> GetMy()
        {
            try
            {
                var userId = HttpContext.User.Claims.Where(x => x.Type.Contains("nameidentifier")).Single().Value;
                var data = await _recipesData.GetByUserId(userId);
                var mappedRecipes = _mapper.Map<IEnumerable<RecipeViewModel>>(data);
                return View(mappedRecipes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error");
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                _logger.LogInformation("Deleting recipe with Id: {id}", id);
                await _recipesData.Delete(id);
                _logger.LogInformation("Recipe with Id: {id} has beend properly deleted", id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Cannot delete recipe");
            }

        }
    }
}
