using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public interface IAuthorData
    {
        Task AddAuthorAsync(string id, string name, string surname, string email, string phoneNumber);
    }
}