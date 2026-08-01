using EcommWebAPI.Models;
namespace EcommWebAPI.EcomBL
{
    public interface IUserAccountService
    {
        Task<IEnumerable<UserAccount>> GetAllUserAccounts();
        Task<UserAccount> GetUserAccountById(int id);
        Task<int> CreateUserAccount(UserAccount userAccount);
        Task<bool> UpdateUserAccount(UserAccount userAccount);
        Task<bool> DeleteUserAccount(int id);
    }
}
