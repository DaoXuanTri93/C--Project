namespace CityInfo.API.Models.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = "mailSetting:mailToAddress";
            _mailFrom = "mailSetting:mailFromAddress";
        }
        public void Send(string subject, string message)
        {
            // send mail - output to console windown
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, + with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
