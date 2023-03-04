using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Computer_Store_API.Helper
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try { 
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("chanel.waelchi@ethereal.email"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            //send email
            using var emailClient = new SmtpClient();
            emailClient.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
            emailClient.Authenticate("chanel.waelchi@ethereal.email", "XWhgk4NuBBqHhnsnHd");
            emailClient.Send(emailToSend);
            emailClient.Disconnect(true);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
