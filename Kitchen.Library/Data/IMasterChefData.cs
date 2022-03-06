using System.Threading.Tasks;

namespace Kitchen.Library.Data
{
    public interface IMasterChefData
    {
        Task AddMasterChefAsync(string id, string name, string surname, string email, string phoneNumber);
    }
}