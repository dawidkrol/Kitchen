using Kitchen.Library.DataModels;
using Kitchen.Library.DbAccess;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CategoryStructData> _logger;

        public CategoryStructData(ISqlDataAccess data, ILogger<CategoryStructData> logger)
        {
            _data = data;
            _logger = logger;
        }
        public async Task<IEnumerable<CategoryData>> GetCategories()
        {
            var a =  await _data.LoadDataAsyncViews<CategoryData>("select * from [dbo].[vwCategories_Get]");

            //Faster way
            Parallel.ForEach(a, x =>
                {
                    lock (x)
                    {
                        x.SubDirectories = (GetSubdirectoriesById(x.Id).GetAwaiter().GetResult()).ToList();
                    }
                }
            );


            //foreach (var x in a)
            //{
            //    x.SubDirectories = (await GetSubdirectoriesById(x.Id)).ToList();
            //}
            return a;
        }
        public async Task<IEnumerable<SubcategoriesData>> GetSubdirectoriesById(int id)
        {
            return await _data.LoadDataAsync<SubcategoriesData, dynamic>("[dbo].[spSubcategories_GetByCategoryId]", new { CategoryId = id });
        }
        public async Task AddSubdirectory(SubcategoriesData data)
        {
            throw new NotImplementedException();
        }
    }
}
