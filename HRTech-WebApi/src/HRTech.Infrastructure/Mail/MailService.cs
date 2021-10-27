using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Services.Mail.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HRTech.Infrastructure.Mail
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;

        public MailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task Send(string recipient, string subject, string message, CancellationToken cancellationToken = default)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("HR-Tech_System", _settings.Address));
            mimeMessage.To.Add(new MailboxAddress("", recipient));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port, true, cancellationToken);
            await client.AuthenticateAsync(_settings.Address, _settings.Password, cancellationToken);
            await client.SendAsync(mimeMessage, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);        
        }
    }
}