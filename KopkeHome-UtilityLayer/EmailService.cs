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

                        Console.WriteLine($"Host: {_config["EmailConfig:host"]}");
                        Console.WriteLine($"Port: {_config["EmailConfig:port"]}");
                        Console.WriteLine($"Username: {_config["EmailConfig:Username"]}");
                        Console.WriteLine($"Sender: {_config["EmailConfig:EmailSender"]}");

                        smtpClient.Timeout = 10000;

                        Console.WriteLine("About to send...");

                        smtpClient.Send(mailMessage);

                        Console.WriteLine("Mail sent.");

                        isSend = true;
                    }
                }
            }
            catch(Exception ex)
            {
                // throw;
                Console.WriteLine(ex.ToString());
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
            catch(Exception ex)
            {
                // throw;
                Console.WriteLine(ex.ToString());
                throw;
            }

        }








    }
}












