using BeerRosterAPI.DTOs;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace BeerRosterAPI.Services
{
    public class EmailService : IEmailService
    {
        public async Task<Response> Send(EmailDto email)
        {
            //var apiKey = Environment.GetEnvironmentVariable("SendGridKey");

            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("test@example.com", "Example User");
            //var subject = email.Subject;
            //var to = new EmailAddress(email.ToEmail, "Example User");
            //var plainTextContent = email.Message;
            //var htmlContent = "<strong>Cheers...</strong>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);

            //return response;
            return null;
        }
    }
}
