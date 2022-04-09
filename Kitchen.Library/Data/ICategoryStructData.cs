using Kitchen.Library.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public interface ICategoryStructData
    {
        Task AddSubdirectory(SubcategoriesData data);
        Task<IEnumerable<CategoryData>> GetCategories();
        Task<IEnumerable<SubcategoriesData>> GetSubdirectoriesById(int id);
    }
}