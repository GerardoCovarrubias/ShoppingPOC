namespace BusinessLogic.Contracts
{
    public interface ILoginService
    {
        Task<bool> ValidateUserAsync(string email, string password);
        
        Task ChangePasswordAsync(string email, string newPassword);
    }
}
