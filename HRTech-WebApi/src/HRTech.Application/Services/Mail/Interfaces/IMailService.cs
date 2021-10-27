using System.Threading;
using System.Threading.Tasks;

namespace HRTech.Application.Services.Mail.Interfaces
{
    public interface IMailService
    {
        Task Send(string recipient, string subject, string message, CancellationToken cancellationToken = default);
    }
}