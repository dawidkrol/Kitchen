﻿using Kitchen.Library.DataModels;
using Kitchen.Library.DbAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public class RecipesData : IRecipesData
    {
        private readonly ISqlDataAccess _data;

        public RecipesData(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<IEnumerable<RecipeData>> Get(int IdPodkategorii)
        {
            return await _data.LoadDataAsync<RecipeData, dynamic>("[dbo].[spPrzepisy_GetByPodkategorieId]", new { IdPodkategorii });
        }

        public async Task<RecipeData> GetById(int Id)
        {
            return (await _data.LoadDataAsync<RecipeData, dynamic>("[dbo].[spPrzepisy_GetById]", new { Id })).FirstOrDefault();
        }

        public async Task<IEnumerable<RecipeData>> GetByUserId(string userId)
        {
            return await _data.LoadDataAsync<RecipeData, dynamic>("[dbo].[spPrzepisy_GetuserId]", new { UserId = userId });
        }

        public async Task Add(RecipeData data)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spPrzepisy_Add]", new { 
                    Nazwa = data.Nazwa,
                    Przepis = data.Przepis,
                    Idpodkategorii = data.IdPodkategorii,
                    IdPochodzenia = data.IdPochodzenia,
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
        public async Task Delete(int id)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spPrzepisy_Del]", new { Id = id });
        } 
    }
}