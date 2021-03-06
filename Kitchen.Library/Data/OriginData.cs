using Kitchen.Library.DataModels;
using Kitchen.Library.DbAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public class OriginData : IOriginData
    {
        private readonly ISqlDataAccess _data;

        public OriginData(ISqlDataAccess data)
        {
            _data = data;
        }
        public async Task<IEnumerable<RegionDataModel>> Get()
        {
            return await _data.LoadDataAsyncViews<RegionDataModel>("select * from [dbo].[vwOrigins_Get]");
        }

    }
}
