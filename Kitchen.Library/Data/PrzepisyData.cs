using Kitchen.Library.DataModels;
using Kitchen.Library.DbAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public class PrzepisyData : IPrzepisyData
    {
        private readonly ISqlDataAccess _data;

        public PrzepisyData(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<IEnumerable<PrzepisData>> Get(int IdPodkategorii)
        {
            return await _data.LoadDataAsync<PrzepisData, dynamic>("[dbo].[spPrzepisy_GetByPodkategorieId]", new { IdPodkategorii });
        }

        public async Task<PrzepisData> GetById(int Id)
        {
            return (await _data.LoadDataAsync<PrzepisData, dynamic>("[dbo].[spPrzepisy_GetById]", new { Id })).FirstOrDefault();
        }

        public async Task Add(PrzepisData data)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spPrzepisy_Add]", new { 
                    Nazwa = data.Nazwa,
                    Przepis = data.Przepis,
                    Idpodkategorii = data.IdPodkategorii,
                    Region = data.Region,
                    Kraj = data.Kraj,
                    LiczbaPorcji = data.LiczbaPorcji,
                    SzacowanaWartosc = data.SzacowanaWartosc,
                    BialkoWPorcji = data.BialkoWPorcji,
                    WartoscKalJednejPorcji = data.WartoscKalJednejPorcji,
                    TluszczeWPorcji = data.TluszczeWPorcji,
                    WeglowodanyWPorcji = data.WeglowodanyWPorcji,
                    WartoscJednejPorcji = data.SzacowanaWartosc/data.LiczbaPorcji,
                    UserId = data.UserId
            });
        }
    }
}
