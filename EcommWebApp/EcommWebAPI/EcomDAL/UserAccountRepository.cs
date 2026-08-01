using EcommWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommWebAPI.EcomDAL
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly EcommDbContext _context;

        public UserAccountRepository(EcommDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserAccount>> GetAllUserAccounts()
        {
            try
            {
                return await _context.UserAccounts.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<UserAccount>();
            }
        }

        public async Task<UserAccount> GetUserAccountById(int id)
        {
            try
            {
                var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserAccountId == id);
                if (user == null)
                    throw new Exception("User not found");
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> CreateUserAccount(UserAccount userAccount)
        {
            try
            {
                _context.UserAccounts.Add(userAccount);
                await _context.SaveChangesAsync();
                return userAccount.UserAccountId;
            }
            catch
            {
                return -1;
            }

        }

        public async Task<bool> UpdateUserAccount(UserAccount userAccount)
        {
            try
            {
                var existingUserAccount = await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserAccountId == userAccount.UserAccountId);
                if (existingUserAccount == null)
                    return false;

                existingUserAccount.FullName = userAccount.FullName;
                existingUserAccount.UserName = userAccount.UserName;
                existingUserAccount.UserEmail = userAccount.UserEmail;
                existingUserAccount.Password = userAccount.Password;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> DeleteUserAccount(int id)
        {
            try
            {
                var userAccount = await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserAccountId == id);
                if (userAccount == null)
                    return false;

                _context.UserAccounts.Remove(userAccount);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
