using Kitchen.Library.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public interface IRecipesData
    {
        Task<IEnumerable<RecipeData>> Get(int SubcategoryId);
        Task<RecipeData> GetById(int Id);
        Task Add(RecipeData data);
        Task Delete(int id);
        Task<IEnumerable<RecipeData>> GetByUserId(string userId);
    }
}