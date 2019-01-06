using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlbergueAnimal.Data;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using AlbergueAnimal.Areas.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore;

namespace AlbergueAnimal.Controllers
{
    public class AdocaoController : Controller
    {
        private SignInManager<Utilizador> SignInManager;
        private UserManager<Utilizador> UserManager;
        private readonly ApplicationDbContext _context;
        private readonly EmailSenderAdoption _emailSender;
       
        public AdocaoController(ApplicationDbContext context)
        {
            _context = context;
            _emailSender = new EmailSenderAdoption();
        }

        // GET: Adocao
        public IActionResult Index(string searchString)
        {
            var AdocoesArquivadas = from d in _context.Adocao.Include(a => a.Animal).Include(a => a.EstadoAdocao).Include(a => a.Utilizador) select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                AdocoesArquivadas = AdocoesArquivadas.Where(s => s.Animal.Nome.Contains(searchString) || s.UserName.Contains(searchString)
                || s.EstadoAdocao.estado.Contains(searchString));
            }

            return View(AdocoesArquivadas.ToList());
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult IndexArquivo()
        {
            var AdocoesArquivadas = from d in _context.Adocao.Include(a => a.Animal).Include(a => a.EstadoAdocao).Include(a => a.Utilizador) select d;

            AdocoesArquivadas = AdocoesArquivadas.Where(d => d.Arquivado == true);

            return View(AdocoesArquivadas.ToList());
        }

        // GET: Adocao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adocao = await _context.Adocao
                .Include(a => a.Animal)
                .Include(a => a.EstadoAdocao)
                .Include(a => a.Utilizador)
                .FirstOrDefaultAsync(m => m.AdocaoId == id);
            if (adocao == null)
            {
                return NotFound();
            }

            return View(adocao);
        }

        // GET: Adocao/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Set<Animal>(), "AnimalId", "Nome");
            ViewData["EstadoAdocaoId"] = new SelectList(_context.Set<EstadoAdocao>(), "EstadoAdocaoId", "estado");
            ViewData["UserName"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Adocao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdocaoId,AnimalId,UserName,EstadoAdocaoId,CreationDate,LastUpdated,EndDate")] Adocao adocao)
        {
            if (ModelState.IsValid)
            {
                //  list.Any(cus => cus.FirstName == "John");
                var result =_context.Adocao.ToList().Where(z => z.EstadoAdocaoId.Equals(1)).Any(a => a.AnimalId==adocao.AnimalId);
                if(result==false)
                {
                    //var x = User.Identity.Name;
                    //User.Identity.Name;
                    //adocao.UserName = x.ToString();
                    adocao.CreationDate = DateTime.Now;
                    adocao.LastUpdated = DateTime.Now;
                    adocao.EndDate = null;
                    adocao.Arquivado = false;//*******
                    adocao.EstadoAdocaoId = 2;
                    _context.Add(adocao);
                    //adocao.UserName = UserManager.GetUserId(User);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["AnimalId"] = new SelectList(_context.Set<Animal>(), "AnimalId", "Nome", adocao.AnimalId);
                    ViewData["EstadoAdocaoId"] = new SelectList(_context.Set<EstadoAdocao>(), "EstadoAdocaoId", "estado", adocao.EstadoAdocaoId);
                    ViewData["UserName"] = new SelectList(_context.Users, "Id", "UserName", adocao.UserName);
                    //ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Cor", adocao.AnimalId);
                    //ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "EstadoAdocaoId", adocao.EstadoAdocaoId);
                    //ViewData["UserName"] = new SelectList(_context.Users, "Id", "Id", adocao.UserName);
                    return View(adocao);
                }
                
                
            }
            ViewData["AnimalId"] = new SelectList(_context.Set<Animal>(), "AnimalId", "Nome", adocao.AnimalId);
            ViewData["EstadoAdocaoId"] = new SelectList(_context.Set<EstadoAdocao>(), "EstadoAdocaoId", "estado",adocao.EstadoAdocaoId);
            ViewData["UserName"] = new SelectList(_context.Users, "Id", "UserName", adocao.UserName);
            //ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Cor", adocao.AnimalId);
            //ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "EstadoAdocaoId", adocao.EstadoAdocaoId);
            //ViewData["UserName"] = new SelectList(_context.Users, "Id", "Id", adocao.UserName);
            return View(adocao);
        }

        // GET: Adocao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adocao = await _context.Adocao.FindAsync(id);
            if (adocao == null)
            {
                return NotFound();
            }
            //ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Cor", adocao.AnimalId);
            //ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "EstadoAdocaoId", adocao.EstadoAdocaoId);
            //ViewData["UserName"] = new SelectList(_context.Users, "Id", "Id", adocao.UserName);
            ViewData["AnimalId"] = new SelectList(_context.Set<Animal>(), "AnimalId", "Nome",adocao.AnimalId);
            ViewData["EstadoAdocaoId"] = new SelectList(_context.Set<EstadoAdocao>(), "EstadoAdocaoId", "estado",adocao.EstadoAdocaoId);
            ViewData["UserName"] = new SelectList(_context.Users, "Id", "UserName",adocao.UserName);
            return View(adocao);
        }

        // POST: Adocao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdocaoId,AnimalId,UserName,EstadoAdocaoId,CreationDate,LastUpdated,EndDate")] Adocao adocao)
        {
            if (id != adocao.AdocaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    adocao.LastUpdated = DateTime.Now;
                    _context.Update(adocao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdocaoExists(adocao.AdocaoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (adocao.EstadoAdocaoId.Equals(4))
                {
                    adocao.EndDate = DateTime.Now;
                    //adocao.Arquivado = true;
                    //adocao.Animal.Arquivado = true;
                    var x = _context.Users.Where(a => a.Id == adocao.UserName);
                    _emailSender.SendEmailAdoption(x.First().ToString(), "Adoção", $"A sua adoção foi aceite com sucesso. Obrigado por contribuir para o bem dos nossos animais! <br/>Poderá vir levantar o seu novo amigo a qualquer altura do nosso horário de atendimento.");
                }
               
                    return RedirectToAction(nameof(Index));
                
            }
            ViewData["AnimalId"] = new SelectList(_context.Set<Animal>(), "AnimalId", "Nome", adocao.AnimalId);
            ViewData["EstadoAdocaoId"] = new SelectList(_context.Set<EstadoAdocao>(), "EstadoAdocaoId", "estado", adocao.EstadoAdocaoId);
            ViewData["UserName"] = new SelectList(_context.Users, "Id", "UserName", adocao.UserName);
            //ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Cor", adocao.AnimalId);
            //ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "EstadoAdocaoId", adocao.EstadoAdocaoId);
            //ViewData["UserName"] = new SelectList(_context.Users, "Id", "Id", adocao.UserName);
            return View(adocao);
        }

        // GET: Adocao/Delete/5
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adocao = await _context.Adocao
                .Include(a => a.Animal)
                .Include(a => a.EstadoAdocao)
                .Include(a => a.Utilizador)
                .FirstOrDefaultAsync(m => m.AdocaoId == id);
            if (adocao == null)
            {
                return NotFound();
            }

            return View(adocao);
        }

        // POST: Adocao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adocao = await _context.Adocao.FindAsync(id);
            adocao.Arquivado = true;
            //_context.Adocao.Remove(adocao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdocaoExists(int id)
        {
            return _context.Adocao.Any(e => e.AdocaoId == id);
        }

        public ActionResult IndexById(int id)
        {
            var emp = _context.Adocao.Where(e => e.AdocaoId == id).First();
            return View(emp);
        }
        public ActionResult PrintAdoptionSlip(int id)
        {
            var emp = _context.Adocao.Where(e => e.AdocaoId == id).First();
            var report = new Rotativa.AspNetCore.ViewAsPdf("Details", emp);
            return report;
        }

    }
}
