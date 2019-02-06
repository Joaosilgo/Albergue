
using AlbergueAnimal.Data;
using AlbergueAnimal.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace AlbergueAnimal.Areas.Identity.Services
{
    public class EmailSenderStock

    {

        private readonly ApplicationDbContext _context;
        public EmailSenderStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SendEmailAsync(string subject, string message)
        {
            var stockadmsis = _context.Roles.Where(a => a.Name.Equals("administrator")  ||  a.Name.Equals("Gestor Stock") );
            foreach (IdentityRole element in stockadmsis)
            {
                var x = _context.UserRoles.Where(b => b.RoleId.Equals(element.Id));
                 
                foreach (IdentityUserRole<string> i in x)
                {
                    var users = _context.Users.Where(s => s.Id.Equals(i.UserId));


                    foreach (Utilizador item in users)
                    {
                        string body;
                        //Read template file from the App_Data folder
                        var sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Templates/emailStock.html"));

                        body = sr.ReadToEnd();


                        var msg = new MimeMessage();
                        msg.From.Add(new MailboxAddress("Gestão de Stock", "m7.gpr.1718@gmail.com"));
                        msg.To.Add(new MailboxAddress("User", item.Email));
                        msg.Subject = subject + ", Albergue Animais";
                        msg.Body = new TextPart("html")
                        {

                            Text = body
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
                    //  return Task.CompletedTask;

                }
            }



        }
    }
}

//for ()





