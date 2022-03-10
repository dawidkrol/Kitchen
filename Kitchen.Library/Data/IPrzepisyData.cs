using Kitchen.Library.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public interface IPrzepisyData
    {
        Task<IEnumerable<PrzepisData>> Get(int IdPodkategorii);
        Task<PrzepisData> GetById(int Id);
        Task Add(PrzepisData data);
        Task Delete(PrzepisData data);
    }
}