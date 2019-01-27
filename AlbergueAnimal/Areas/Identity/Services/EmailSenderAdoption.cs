using AlbergueAnimal.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
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


        public void SendEmailAdoption(string userId, string subject, string message)
        {
           var x= _context.Users.Where(a=>a.Id.Equals(userId)).First();
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("Adoção", "m7.gpr.1718@gmail.com"));
            msg.To.Add(new MailboxAddress("User", x.Email.ToString()));
            msg.Subject = subject + ", Albergue Animais";
            msg.Body = new TextPart("html")
            {

                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.Auto);
                client.Authenticate("m7.gpr.1718@gmail.com", "gpr_m7_1718");
                client.Send(msg);
                client.Disconnect(true);
            }

        }
    }
}
