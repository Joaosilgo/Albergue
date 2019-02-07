using AlbergueAnimal.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Areas.Identity.Services
{
    public class EmailSenderAdoption
    {
        private readonly ApplicationDbContext _context;
        public EmailSenderAdoption(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task SendEmailAdoptionAsync(string userId, string subject, string message)
        {
           var x= _context.Users.Where(a=>a.Id.Equals(userId)).First();
            string body;
            //Read template file from the App_Data folder
            var sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Templates/emailAdoption.html"));

            body = sr.ReadToEnd();

            //var msg = new MimeMessage();
            //msg.From.Add(new MailboxAddress("Adoção", "m7.gpr.1718@gmail.com"));
            //msg.To.Add(new MailboxAddress("User", x.Email.ToString()));
            //msg.Subject = subject + ", Albergue Animais";
            //msg.Body = new TextPart("html")
            //{

            //    Text = "oi"
            //};

            //using (var client = new SmtpClient())
            //{
            //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            //    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.Auto);
            //    client.Authenticate("m7.gpr.1718@gmail.com", "gpr_m7_1718");
            //    client.Send(msg);
            //    client.Disconnect(true);
            //}



            //   var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("noreply@albergue.com", "Quinta do Mião"),
                Subject = "Adoções!",
                //  PlainTextContent = "Hello, Email!",
                HtmlContent = body
            };
            msg.AddTo(new EmailAddress(x.Email.ToString(), "Test User"));
            var response = await client.SendEmailAsync(msg);


        }
    }
}
