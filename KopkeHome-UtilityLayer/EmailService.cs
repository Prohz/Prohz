using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace KopkeHome_UtilityLayer
{

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration configuration)
        {

            _config = configuration;
        }
        public bool SendEmail(string mailto, string subject, string MailBody)
        {
            bool isSend = false;
            SmtpClient smtpClient;
            MailMessage mailMessage;
            try
            {
                using (smtpClient = new SmtpClient())
                {
                    smtpClient.Host = _config["EmailConfig:host"];
                    smtpClient.Port = Convert.ToInt32(_config["EmailConfig:port"]);

                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(_config["EmailConfig:Username"], _config["EmailConfig:Password"]);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    using (mailMessage = new MailMessage(_config["EmailConfig:EmailSender"], mailto))
                    {
                        mailMessage.Subject = subject;
                        mailMessage.Body = MailBody;
                        mailMessage.IsBodyHtml = true;
                        smtpClient.Send(mailMessage);
                        isSend = true;
                    }
                }
            }
            catch
            {
                throw;
            }
            return isSend;
        }
        public string GenerateOTPForAuthentication()
        {
            try
            {
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                string AuthenticationCode = Convert.ToString(r);
                return AuthenticationCode;
            }
            catch
            {
                throw;
            }

        }








    }
}












