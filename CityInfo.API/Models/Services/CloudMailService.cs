namespace CityInfo.API.Models.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = "mailSetting:mailToAddress";
            _mailFrom = "mailSetting:mailFromAddress";
        }
        public void Send(string subject, string message)
        {
            // send mail - output to console windown
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}," +
                $" + with {nameof(CloudMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
