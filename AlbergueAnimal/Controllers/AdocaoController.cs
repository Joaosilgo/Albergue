using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlbergueAnimal.Data;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Identity;
using AlbergueAnimal.Areas.Identity.Services;
using Microsoft.AspNetCore.Authorization;

namespace AlbergueAnimal.Controllers
{
    public class AdocaoController : Controller
    {

        private readonly UserManager<Utilizador> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly EmailSenderAdoption _emailSender;

        public AdocaoController(ApplicationDbContext context, UserManager<Utilizador> userManager)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = new EmailSenderAdoption(context);
        }

        // GET: Adocao
        public async Task<IActionResult> Index(string searchString)
        {
            var AdocoesArquivadas = from d in _context.Adocao.Include(a => a.Animal).Include(a => a.EstadoAdocao).Include(a => a.Utilizador).Where(a => a.Arquivado == false && a.EstadoAdocao.estado.Equals("Pendente")) select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                AdocoesArquivadas = AdocoesArquivadas.Where(s => s.Animal.Nome.Contains(searchString) || s.UserId.Contains(searchString)
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


        public IActionResult IndexEmProcessamento()
        {
            var AdocoesEmProcessamento = from d in _context.Adocao.Include(a => a.Animal).Include(a => a.EstadoAdocao).Include(a => a.Utilizador) select d;

            AdocoesEmProcessamento = AdocoesEmProcessamento.Where(d => d.Arquivado == false && d.EstadoAdocao.estado.Equals("EmProcessamento"));

            return View(AdocoesEmProcessamento.ToList());
        }


        public IActionResult IndexAnimalPerUser()
        {
            var varAdocao = from d in _context.Adocao.Include(a => a.Animal).Include(a => a.EstadoAdocao).Include(a => a.Utilizador) select d;

            varAdocao = varAdocao.Where(d => d.Arquivado == false && d.Utilizador.Id.Equals(_userManager.GetUserId(User)));
            
            return View(varAdocao.ToList());
            
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
            ViewData["AnimalId"] = new SelectList(_context.Animal.Where(a => a.Arquivado == false), "AnimalId", "Nome");
            ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "estado");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Adocao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdocaoId,AnimalId,UserId,EstadoAdocaoId,CreationDate,LastUpdated,EndDate,Arquivado")] Adocao adocao)
        {
            if (ModelState.IsValid)
            {
                var result = _context.Adocao.ToList().Where(z => z.EstadoAdocaoId.Equals(1)).Any(a => a.AnimalId == adocao.AnimalId);
                if (result == false)
                {
                    adocao.CreationDate = DateTime.Now;
                    adocao.LastUpdated = DateTime.Now;
                    adocao.EndDate = null;
                    adocao.Arquivado = false;
                    adocao.EstadoAdocaoId = 2;
                    _context.Add(adocao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Nome", adocao.AnimalId);
            ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "estado", adocao.EstadoAdocaoId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", adocao.UserId);
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
            ViewData["AnimalId"] = new SelectList(_context.Animal.Where(a => a.Arquivado == false), "AnimalId", "Nome", adocao.AnimalId);
            ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "estado", adocao.EstadoAdocaoId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", adocao.UserId);
            return View(adocao);
        }

        // POST: Adocao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdocaoId,AnimalId,UserId,EstadoAdocaoId,CreationDate,LastUpdated,EndDate,Arquivado")] Adocao adocao)
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
                    adocao.Arquivado = true;
                    await _context.SaveChangesAsync();
                    var d = _context.Animal.Where(a => a.AnimalId == adocao.AnimalId).First();
                    d.Arquivado = true;
                    await _emailSender.SendEmailAdoptionAsync(adocao.UserId, "", "");
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal.Where(a => a.Arquivado == false), "AnimalId", "Nome", adocao.AnimalId);
            ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "estado", adocao.EstadoAdocaoId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", adocao.UserId);
            return View(adocao);
        }

        // GET: Adocao/Delete/5
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
            _context.Adocao.Remove(adocao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdocaoExists(int id)
        {
            return _context.Adocao.Any(e => e.AdocaoId == id);
        }




        public async Task<IActionResult> IndexById(int? id)
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
        public ActionResult PrintAdoptionSlip(int id)
        {
            //  var emp = _context.Adocao.Where(e => e.AdocaoId == id).First();
            var adocao = _context.Adocao
                  .Include(a => a.Animal)
                  .Include(a => a.EstadoAdocao)
                  .Include(a => a.Utilizador)
                  .Where(m => m.AdocaoId == id).First();

            var report = new Rotativa.AspNetCore.ViewAsPdf("IndexById", adocao);
            return report;
        }


        public async Task<IActionResult> Arquivar(int? id)
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
        [HttpPost, ActionName("Arquivar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArquivarConfirmed(int id)
        {
            var adocao = await _context.Adocao.FindAsync(id);
            // _context.Adocao.Remove(adocao);
            adocao.Arquivado = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






























        // GET: Adocao/Create
        public IActionResult CreatePerUser()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal.Where(a => a.Arquivado == false), "AnimalId", "Nome");
            ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "estado");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Adocao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePerUser([Bind("AdocaoId,AnimalId,UserId,EstadoAdocaoId,CreationDate,LastUpdated,EndDate,Arquivado")] Adocao adocao)
        {
            if (ModelState.IsValid)
            {
                var result = _context.Adocao.ToList().Where(z => z.EstadoAdocaoId.Equals(1)).Any(a => a.AnimalId == adocao.AnimalId);
                if (result == false)
                {
                    adocao.CreationDate = DateTime.Now;
                    adocao.LastUpdated = DateTime.Now;
                    adocao.EndDate = null;
                    adocao.Arquivado = false;
                    adocao.EstadoAdocaoId = 2;
                    _context.Add(adocao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexAnimalPerUser));
                }
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Nome", adocao.AnimalId);
            ViewData["EstadoAdocaoId"] = new SelectList(_context.EstadoAdocao, "EstadoAdocaoId", "estado", adocao.EstadoAdocaoId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", adocao.UserId);
            return View(adocao);
        }



    }
}
