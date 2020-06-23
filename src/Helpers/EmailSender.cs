using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uption.Helpers
{
    public class EmailSender
    {
        public void SendEmail(List<string> recipients, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            foreach (var recipient in recipients)
            {
                emailMessage.To.Add(new MailboxAddress("", recipient));
            }

            emailMessage.From.Add(new MailboxAddress("Uption Company", "uption.it@gmail.com"));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("uption.it@gmail.com", "upt123321UPT");
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
    }
}
