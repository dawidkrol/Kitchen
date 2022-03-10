using Kitchen.Library.DataModels;
using Kitchen.Library.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public class PochodzenieData : IPochodzenieData
    {
        private readonly ISqlDataAccess _data;

        public PochodzenieData(ISqlDataAccess data)
        {
            _data = data;
        }
        public async Task<IEnumerable<RegionDataModel>> Get()
        {
            return await _data.LoadDataAsync<RegionDataModel, dynamic>("[dbo].[spPochodzenie_Get]", new { });
        }

    }
}
