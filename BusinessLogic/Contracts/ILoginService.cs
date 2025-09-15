namespace BusinessLogic.Contracts
{
    public interface ILoginService
    {
        Task<bool> ValidateUserAsync(string username, string password);
        
        Task ChangePasswordAsync(string username, string newPassword);
    }
}
