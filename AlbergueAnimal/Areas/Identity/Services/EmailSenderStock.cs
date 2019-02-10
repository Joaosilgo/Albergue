
using AlbergueAnimal.Data;
using AlbergueAnimal.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
    public class EmailSenderStock

    {

        private readonly ApplicationDbContext _context;
        public EmailSenderStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendEmailAsync(string subject, string message)
        {
            //var stockadmsis = _context.Roles.Where(a => a.Name.Equals("administrator")  /*&&  a.Name.Equals("Gestor Stock")*/ ).ToList();

            //foreach (IdentityRole element in stockadmsis)
            //{
            //    var x = _context.UserRoles.Where(b => b.RoleId.Equals(element.Id));

            //    foreach (IdentityUserRole<string> i in x)
            //    {
            //        var users = _context.Users.Where(s => s.Id.Equals(i.UserId));
            var stockManager = _context.Users.Where(a => a.Cargo.Equals("Administrator")).ToList();

                    foreach (Utilizador item in stockManager)
                    {
                //string body;
                ////Read template file from the App_Data folder
                //var sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Templates/emailStock.html"));

                //body = sr.ReadToEnd();


                //var msg = new MimeMessage();
                //msg.From.Add(new MailboxAddress("Gestão de Stock", "m7.gpr.1718@gmail.com"));
                //msg.To.Add(new MailboxAddress("User", item.Email));
                //msg.Subject = subject + ", Albergue Animais";
                //msg.Body = new TextPart("html")
                //{

                //    Text = body
                //};

                //using (var client = new SmtpClient())
                //{
                //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.Auto);
                //    client.Authenticate("m7.gpr.1718@gmail.com", "gpr_m7_1718");
                //    client.Send(msg);
                //    client.Disconnect(true);
                //}
                var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
                // var apiKey = "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";
                var client = new SendGridClient(apiKey);
                        var msg = new SendGridMessage()
                        {
                            From = new EmailAddress("noreply@albergue.com", "Quinta do Mião"),
                            Subject = "Stock!",
                            // PlainTextContent = body
                            HtmlContent = "ola"
                        };
                        //  
                        msg.AddTo(new EmailAddress(item.Email, "Test User"));
                        if (!string.IsNullOrEmpty("d-a0baa4e4762c4dc4837580fc07cbf0bb"))
                            msg.TemplateId = "d-a0baa4e4762c4dc4837580fc07cbf0bb";
                        var response = await client.SendEmailAsync(msg);
                    }
                    //  return Task.CompletedTask;

                }
            }



        }



//////    }
//////}

//for ()





