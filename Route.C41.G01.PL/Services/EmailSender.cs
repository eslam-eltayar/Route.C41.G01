using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Route.C41.G01.PL.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendAsync(string from, string to, string Subject, string body)
        {
            var senderEmail = _configuration["EmailSettings:senderEmail"];
            var senderPassword = _configuration["EmailSettings:SenderPassword"];

            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(from);
            emailMessage.To.Add(to);
            emailMessage.Subject = Subject;
            emailMessage.Body = $"<html><body>{body}<body></html>"; // we can send HTML page
            emailMessage.IsBodyHtml = true;


            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpClientServer"],int.Parse( _configuration["EmailSettings:SmtpClientPort"]) )
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true

            
            };

            await smtpClient.SendMailAsync(emailMessage);     
                
        }
    }
}
