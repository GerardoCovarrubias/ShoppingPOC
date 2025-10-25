using BusinessLogic.Contracts;
using System.Net;
using System.Net.Mail;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILoginService _loginService;

        public EmailService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async void SendMail(string useremail, string subject, string body)
        {
            try
            {

                if (await _loginService.UserExistsAsync(useremail))
                {

                    var mail = new MailMessage();
                    mail.From = new MailAddress("isainokia@gmail.com");
                    mail.To.Add(useremail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("isainokia@gmail.com", "ftpn focn knbq swsv");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                throw;
            }

        }
    }
}
