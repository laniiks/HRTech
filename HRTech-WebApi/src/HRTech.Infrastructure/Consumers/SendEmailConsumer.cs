using System.Threading.Tasks;
using HRTech.Application.Services.Mail.Interfaces;
using HRTech.Application.Services.User.Contracts;
using MassTransit;

namespace HRTech.Infrastructure.Consumers
{
    public class SendEmailConsumer : IConsumer<SendNotification>
    {
        private readonly IMailService _mailService;

        public SendEmailConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<SendNotification> context)
        {
            var sendEmailMessage = context.Message;

            if (sendEmailMessage != null)
            {
                await _mailService.Send(sendEmailMessage.Recipient, sendEmailMessage.Subject, sendEmailMessage.Message);
            }
        }
    }
}