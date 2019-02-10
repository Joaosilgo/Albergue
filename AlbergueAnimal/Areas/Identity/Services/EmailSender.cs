using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimais.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            //var msg = new MimeMessage();
            //msg.From.Add(new MailboxAddress("Email Confirmação", "m7.gpr.1718@gmail.com"));
            //msg.To.Add(new MailboxAddress("User", email));
            //msg.Subject = subject + ", Albergue Animais";
            //msg.Body = new TextPart("html")
            //{

            //    Text = message
            //};

            //using (var client = new SmtpClient())
            //{
            //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            //    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.Auto);
            //    client.Authenticate("m7.gpr.1718@gmail.com", "gpr_m7_1718");
            //    client.Send(msg);
            //    client.Disconnect(true);
            //}
            //return Task.CompletedTask;
            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            // var apiKey = "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";
           // var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("noreply@albergue.com", "Quinta do Mião"),
                Subject = subject,
                //  PlainTextContent = "Hello, Email!",
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email, "Test User"));
            var response = await client.SendEmailAsync(msg);

        }

        //    using (var client = new SmtpClient()) {
        //client.ServerCertificateValidationCallback = (s, c, h, e) => true;

        //client.Connect(hostName, port, SecureSocketOptions.Auto);

        // 

























    }

}
