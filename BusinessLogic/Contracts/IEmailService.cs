namespace BusinessLogic.Contracts
{
    public interface IEmailService
    {

        void SendMail(string to, string subject, string body);

    }
}
