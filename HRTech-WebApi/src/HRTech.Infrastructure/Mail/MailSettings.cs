namespace HRTech.Infrastructure.Mail
{
    public class MailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}