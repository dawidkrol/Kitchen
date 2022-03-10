using Kitchen.Library.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public interface IPochodzenieData
    {
        Task<IEnumerable<RegionDataModel>> Get();
    }
}