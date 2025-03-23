
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;

namespace Banking_system.Services.AuthService_d
{
    public class MailService : IMailService
    {
        private readonly IConfiguration config;

        public MailService(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<bool> SendMailAsync(string To, string Subject, string Body)
        {

            string host = config["SmtpSettings:Host"];

            int port = int.Parse(config["SmtpSettings:port"]);

            string userName = config["SmtpSettings:Username"];

            string password = config["SmtpSettings:Password"];
            
            var MailMsg = new MimeMessage();

            MailMsg.From.Add(new MailboxAddress("From", config["SmtpSettings:Username"]));
            MailMsg.To.Add(new MailboxAddress("To", To));
            MailMsg.Subject = Subject;
            MailMsg.Body = new TextPart(TextFormat.Html) { Text = Body };

         
            try
            {
                SmtpClient smtpClient = new SmtpClient();

                await smtpClient.ConnectAsync(host, port, SecureSocketOptions.StartTls);

                await smtpClient.AuthenticateAsync(userName, password);

                await smtpClient.SendAsync(MailMsg);

                await smtpClient.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex) { 

                Console.WriteLine(ex.Message);

                Console.WriteLine(ex.ToString());

                return false;
            }
 
        }
    }
}
