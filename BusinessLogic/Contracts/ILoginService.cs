namespace BusinessLogic.Contracts
{
    public interface ILoginService
    {
        Task<bool> ValidateUserAsync(string email, string password);
        Task<bool> UserExistsAsync(string email);
        Task ChangePasswordAsync(string email, string newPassword);
    }
}
