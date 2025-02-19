using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace newFitnet.Common.EmailService
{
    internal class EmailService(IConfiguration configuration) : IEmailService
    {
        public async Task SendEmail(IEnumerable<string> toAddresses, string  subject, string body, string fromAddress= "subhajitpaul630@gmail.com")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromAddress, fromAddress));
            foreach (var to in toAddresses)
            {
                message.To.Add(new MailboxAddress(to, to));
            }
            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var client = new SmtpClient
            {
                // For demo-purposes, accept all SSL certificates
                ServerCertificateValidationCallback = (_, _, _, _) => true
            };
            //Console.WriteLine(configuration.GetValue<string>("TemplateEmailSettings:Server"));
            //Console.WriteLine(configuration.GetValue<string>("UserId"));
            //Console.WriteLine(configuration.GetValue<string>("TemplateEmailSettings:AppSpecificPass"));
            client.Connect(configuration.GetValue<string>("TemplateEmailSettings:Server"), 587, false);

            client.Authenticate(configuration.GetValue<string>("TemplateEmailSettings:UserId"),
                configuration.GetValue<string>("TemplateEmailSettings:AppSpecificPass"));

            client.Send(message);
            client.Disconnect(true);
        }
    }
}
