using Kitchen.Library.DbAccess;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public class MasterChefData : IMasterChefData
    {
        private readonly ISqlDataAccess _dataAccess;

        public MasterChefData(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task AddMasterChefAsync(string id,string name, string surname, string email, string phoneNumber)
        {
            await _dataAccess.SaveDataAsync<dynamic>("[dbo].[spMasterChef_Add]", new
            {
                Id = id,
                Name = name,
                Surname = surname,
                Email = email,
                PhoneNumber = phoneNumber
            });
        }
    }
}
