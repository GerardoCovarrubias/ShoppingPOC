using BusinessLogic.Contracts;
using System.Net;
using System.Net.Mail;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        public void SendMail(string to, string subject, string body)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress("isainokia@gmail.com");
                mail.To.Add(to);
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
            catch (Exception ex)
            {
                var error = ex.Message;

                throw;
            }

}
}
}
