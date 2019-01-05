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
    public class EstadoAdocaosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadoAdocaosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EstadoAdocaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoAdocao.ToListAsync());
        }

        // GET: EstadoAdocaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoAdocao = await _context.EstadoAdocao
                .FirstOrDefaultAsync(m => m.EstadoAdocaoId == id);
            if (estadoAdocao == null)
            {
                return NotFound();
            }

            return View(estadoAdocao);
        }

        // GET: EstadoAdocaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoAdocaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoAdocaoId,estado")] EstadoAdocao estadoAdocao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoAdocao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoAdocao);
        }

        // GET: EstadoAdocaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoAdocao = await _context.EstadoAdocao.FindAsync(id);
            if (estadoAdocao == null)
            {
                return NotFound();
            }
            return View(estadoAdocao);
        }

        // POST: EstadoAdocaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadoAdocaoId,estado")] EstadoAdocao estadoAdocao)
        {
            if (id != estadoAdocao.EstadoAdocaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoAdocao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoAdocaoExists(estadoAdocao.EstadoAdocaoId))
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
            return View(estadoAdocao);
        }

        // GET: EstadoAdocaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoAdocao = await _context.EstadoAdocao
                .FirstOrDefaultAsync(m => m.EstadoAdocaoId == id);
            if (estadoAdocao == null)
            {
                return NotFound();
            }

            return View(estadoAdocao);
        }

        // POST: EstadoAdocaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoAdocao = await _context.EstadoAdocao.FindAsync(id);
            _context.EstadoAdocao.Remove(estadoAdocao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoAdocaoExists(int id)
        {
            return _context.EstadoAdocao.Any(e => e.EstadoAdocaoId == id);
        }
    }
}
