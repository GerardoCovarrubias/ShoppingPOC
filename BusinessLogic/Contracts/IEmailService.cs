using Models;

namespace BusinessLogic.Contracts
{
    public interface IEmailService
    {

        Task<Result> SendMailAsync(string useremail, string subject, string body);


    }
}
