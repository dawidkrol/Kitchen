using Kitchen.Library.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public interface ICategoryStructData
    {
        Task AddSubdirectory(SubCategoriesData data);
        Task<IEnumerable<CategoryData>> GetCategories();
        Task<IEnumerable<SubCategoriesData>> GetSubdirectoriesById(int id);
    }
}