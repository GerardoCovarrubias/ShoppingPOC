using BusinessLogic.Contracts;
using Models;
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

        public async Task<Result> SendMailAsync(string userEmail, string subject, string body)
        {
            try
            {
                if (!await _loginService.UserExistsAsync(userEmail))
                {
                    return new Result(ResultStatusCode.ProcessError, "The user does not exist.");
                }

                var mail = new MailMessage
                {
                    From = new MailAddress("isainokia@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mail.To.Add(userEmail);

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("isainokia@gmail.com", "ftpn focn knbq swsv");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

                return new Result(ResultStatusCode.Success);
            }
            catch (SmtpException smtpEx)
            {
                return new Result(ResultStatusCode.ProcessError, $"Error SMTP: {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatusCode.Exception, $"Excepción inesperada: {ex.Message}");
            }
        }
    }
}
