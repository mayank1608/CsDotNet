using EcommWebAPI.EcomBL;
using EcommWebAPI.EcomDAL;
using EcommWebAPI.Models;
namespace EcommWebAPI.EcomBL
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _repository;

        public UserAccountService(IUserAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserAccount>> GetAllUserAccounts()
        {
            try
            {
                return await _repository.GetAllUserAccounts();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<UserAccount> GetUserAccountById(int id)
        {
            try
            {
                return await _repository.GetUserAccountById(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<int> CreateUserAccount(UserAccount userAccount)
        {
            try
            {
                return await _repository.CreateUserAccount(userAccount);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<bool> UpdateUserAccount(UserAccount userAccount)
        {
            try
            {
                return await _repository.UpdateUserAccount(userAccount);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<bool> DeleteUserAccount(int id)
        {
            try
            {
                return await _repository.DeleteUserAccount(id);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
