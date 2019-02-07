using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlbergueAnimal.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using AlbergueAnimal.Data;

namespace AlbergueAnimal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            //he = e;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Adote um amigo!";

            return View();
        }

        public async Task<IActionResult> Contact(ContactViewModel vm)
        {
            //ViewData["Message"] = "Pode nos contactar através das seguintes formas";
            if (ModelState.IsValid)
            {
                List<Utilizador> list = new List<Utilizador>();

                if (vm.Subject.Equals("Geral"))
                {
                    list = _context.Users.Where(a => a.Cargo.Equals("Administrator")).ToList();
                }
                foreach (Utilizador item in list)
                {
                    try
                    {

                        var apiKey = "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";
                        var client = new SendGridClient(apiKey);
                        var msg = new SendGridMessage()
                        {
                            From = new EmailAddress(vm.Email, "Quinta do Mião-Notificação Utilizador"),
                            Subject = vm.Subject,
                            HtmlContent = vm.Message
                        };
                        msg.AddTo(new EmailAddress(item.Email, item.Nome));
                        var response = await client.SendEmailAsync(msg);
                        ViewBag.Message = "Obrigado por nos Contactar ";

                    }

                    catch (Exception ex)
                    {
                        ModelState.Clear();
                        ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                    }
                }

            }






            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
