using BeerRosterAPI.DTOs;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace BeerRosterAPI.Services
{
    public class EmailService : IEmailService
    {
        public async Task<Response> Send(EmailDto email)
        {
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var apiKey = "SE.xCin_4MsQ9qYWg1GPyHkQw.me62B3CKBr4eEImR7tPBJ27da58IC838QH1r4nZ_DqM";

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = email.Subject;
            var to = new EmailAddress(email.ToEmail, "Example User");
            var plainTextContent = email.Message;
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response;
        }
    }
}
