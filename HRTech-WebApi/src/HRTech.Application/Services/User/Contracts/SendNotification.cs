namespace HRTech.Application.Services.User.Contracts
{
    public class SendNotification
    {
        public string Recipient { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}