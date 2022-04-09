using Kitchen.Library.DataModels;
using Kitchen.Library.DbAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public class RecipesData : IRecipesData
    {
        private readonly ISqlDataAccess _data;

        public RecipesData(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<IEnumerable<RecipeData>> Get(int SubcategoryId)
        {
            return await _data.LoadDataAsync<RecipeData, dynamic>("[dbo].[spRecipes_GetBySubcategoryId]", new { SubcategoryId });
        }

        public async Task<RecipeData> GetById(int Id)
        {
            return (await _data.LoadDataAsync<RecipeData, dynamic>("[dbo].[spRecipes_GetById]", new { Id })).FirstOrDefault();
        }

        public async Task<IEnumerable<RecipeData>> GetByUserId(string userId)
        {
            return await _data.LoadDataAsync<RecipeData, dynamic>("[dbo].[spRecipes_GetByUserId]", new { UserId = userId });
        }

        public async Task Add(RecipeData data)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spRecipes_Add]", new { 
                    Title = data.Title,
                    Recipe = data.Recipe,
                    Id_Subcategory = data.Id_Subcategory,
                    OriginId = data.OriginId,
                    NumberOfServings = data.NumberOfServings,
                    EstimatedValue = data.EstimatedValue,
                    ProteinsPerServingsInGrams = data.ProteinsPerServingsInGrams,
                    CaloriesPerServingsInGrams = data.CaloriesPerServingsInGrams,
                    FatsPerServingsInGrams = data.FatsPerServingsInGrams,
                    CarbohydratesPerServingsInGrams = data.CarbohydratesPerServingsInGrams,
                    UserId = data.UserId
            });
        }
        public async Task Delete(int id)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spRecipes_Del]", new { Id = id });
        }
    }
}
