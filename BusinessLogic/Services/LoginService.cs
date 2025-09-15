using BusinessLogic.Contracts;
using ShoppingPOC.Data;

namespace BusinessLogic.Services
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task ChangePasswordAsync(string username, string newPassword)
        {
            var user = _dbContext. User.FirstOrDefault(u => u.Username == username);
        }

        public Task<bool> ValidateUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
