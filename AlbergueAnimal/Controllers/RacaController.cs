using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using AlbergueAnimal.Data;
using AlbergueAnimal.Models;

namespace AlbergueAnimal.Controllers
{
    public class RacaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RacaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Raca
        public async Task<IActionResult> Index()
        {
            return View(await _context.Raca.ToListAsync());
        }

        // GET: Raca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raca = await _context.Raca
                .FirstOrDefaultAsync(m => m.RacaId == id);
            if (raca == null)
            {
                return NotFound();
            }

            return View(raca);
        }

        // GET: Raca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Raca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RacaId,Designacao")] Raca raca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raca);
        }

        // GET: Raca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raca = await _context.Raca.FindAsync(id);
            if (raca == null)
            {
                return NotFound();
            }
            return View(raca);
        }

        // POST: Raca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RacaId,Designacao")] Raca raca)
        {
            if (id != raca.RacaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RacaExists(raca.RacaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(raca);
        }

        // GET: Raca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raca = await _context.Raca
                .FirstOrDefaultAsync(m => m.RacaId == id);
            if (raca == null)
            {
                return NotFound();
            }

            return View(raca);
        }

        // POST: Raca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raca = await _context.Raca.FindAsync(id);
            _context.Raca.Remove(raca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RacaExists(int id)
        {
            return _context.Raca.Any(e => e.RacaId == id);
        }
    }
}
