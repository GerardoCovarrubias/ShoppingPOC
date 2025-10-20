using BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            try
            {
                var user = await _dbContext.User
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                return user != null;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task ChangePasswordAsync(string email, string newPassword)
        {
            try
            {
                var user = await _dbContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

           
                    if (user != null)
                    {
                       user.Password = newPassword;
                        user.UpdatedAt = DateTime.Now;
                        await _dbContext.SaveChangesAsync();
                    }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
