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

namespace AlbergueAnimal.Controllers
{
    public class AdocaoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSenderAdoption _emailSender;
       
        public AdocaoController(ApplicationDbContext context)
        {
            _context = context;
            _emailSender = new EmailSenderAdoption();
        }

        // GET: Adocao
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Adocao.Include(a => a.Animal).Include(a => a.EstadoAdocao).Include(a => a.Utilizador);
            return View(await applicationDbContext.ToListAsync());
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
                _context.Add(adocao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
                if (adocao.EstadoAdocaoId.Equals(1))
                {
                    var x = _context.Users.Where(a => a.Id == adocao.UserName);
                    _emailSender.SendEmailAdoption(x.First().ToString(), "Adoption", "cao");
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
    }
}
