using Smart.Core.Helpers.Mails;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Smart.Core.Interface.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(MailRequest mailRequest);

        Task<Response> SendEmailAsync(SendGridMessage message);

        Task SendManyMailsAsync<T>(MailingRequest<T> mailing) where T : class, new();
    }
}
