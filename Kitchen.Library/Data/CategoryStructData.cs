using Kitchen.Library.DataModels;
using Kitchen.Library.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public class CategoryStructData : ICategoryStructData
    {
        private readonly ISqlDataAccess _data;

        public CategoryStructData(ISqlDataAccess data)
        {
            _data = data;
        }
        public async Task<IEnumerable<KategoriaData>> GetCategories()
        {
            var a =  await _data.LoadDataAsyncViews<KategoriaData>("select * from [dbo].[vwKategorie_Get]");
            //Parallel.ForEach(a,async x => x.SubDirectories = (await GetSubdirectoriesById(x.Id)).ToList());
            foreach (var x in a)
            {
                x.SubDirectories = (await GetSubdirectoriesById(x.Id)).ToList();
            }
            return a;
        }
        public async Task<IEnumerable<SubCategoriesData>> GetSubdirectoriesById(int id)
        {
            return await _data.LoadDataAsync<SubCategoriesData, dynamic>("[dbo].[spPodkategorie_GetByKategorieId]", new { IdKategorii = id });
        }
        public async Task AddSubdirectory(SubCategoriesData data)
        {
            throw new NotImplementedException();
        }
    }
}
