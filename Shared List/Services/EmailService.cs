using Azure.Communication.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace Shared_List.Services
{
    public class EmailService : IEmailSender
    {
        private readonly string _connectionString;
        private readonly string _sender;
        public EmailService(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString:Email"];
            _sender = configuration["EmailSender"];
        }
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            EmailClient emailClient = new(_connectionString);
            EmailContent emailContent = new(subject)
            {
                PlainText = htmlMessage,
                Html = htmlMessage
            };

            EmailRecipients emailRecipients = new(new List<EmailAddress> { new EmailAddress(email) });
            EmailMessage emailMessage = new(_sender, emailRecipients, emailContent);
            var emailResult = emailClient.Send(Azure.WaitUntil.Completed, emailMessage, CancellationToken.None).Value;

            return Task.FromResult(emailResult);
        }
    }
}

