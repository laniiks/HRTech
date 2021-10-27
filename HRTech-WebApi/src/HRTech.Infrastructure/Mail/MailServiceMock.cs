using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Services.Mail.Interfaces;

namespace HRTech.Infrastructure.Mail
{
    public class MailServiceMock : IMailService
    {
        public Task Send(string recipient, string subject, string message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
