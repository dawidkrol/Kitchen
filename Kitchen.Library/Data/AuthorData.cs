using Kitchen.Library.DbAccess;
using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public class AuthorData : IAuthorData
    {
        private readonly ISqlDataAccess _dataAccess;

        public AuthorData(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task AddAuthorAsync(string id,string name, string surname, string email, string phoneNumber)
        {
            await _dataAccess.SaveDataAsync<dynamic>("[dbo].[spAuthor_Add]", new
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
