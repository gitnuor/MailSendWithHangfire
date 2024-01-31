using System.Net.Mail;
using System.Net;

namespace EmailSend.Models
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var host = smtpSettings["Host"];
            var port = int.Parse(smtpSettings["Port"]);
            var userName = smtpSettings["UserName"];
            var password = smtpSettings["Password"];
            var enableSsl = bool.Parse(smtpSettings["EnableSsl"]);

            using (var client = new SmtpClient(host, port))
            {
                client.EnableSsl = enableSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, password);

                var message = new MailMessage(userName, to, subject, body);
                message.IsBodyHtml = true;
                client.Timeout = 100000;

                client.Send(message);
            }
        }
    }
}
