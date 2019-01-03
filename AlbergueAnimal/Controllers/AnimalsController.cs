using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using AlbergueAnimal.Data;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AlbergueAnimal.Controllers
{
    /// <summary>
    /// Classe responsável pela definição das ações associadas a um animal.
    /// </summary>
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;

        public AnimalsController(ApplicationDbContext context, IHostingEnvironment e)
        {
            _context = context;
            he = e;
        }

        public IActionResult ShowFields(string fullName, IFormFile pic)
        {
            ViewData["fname"] = fullName;
            if (pic != null)
            {
                var fileName = Path.Combine(he.WebRootPath, Path.GetFileName(pic.FileName));
                pic.CopyTo(new FileStream(fileName, FileMode.Create));
                ViewData["fileLocation"] = "/" + Path.GetFileName(pic.FileName);
            }
            return View();
        }

        public IActionResult ListarNome()
        {
            return View(_context.Animal);
        }

        [HttpPost]
        public IActionResult ListarNome(string filtroNome)
        {
            var nomesAnimaisFiltrados = from a in _context.Animal.Include(a => a.Raca) select a;

            if (!String.IsNullOrEmpty(filtroNome))
            {
                nomesAnimaisFiltrados = nomesAnimaisFiltrados.Where(a => a.Nome.Contains(filtroNome));
            }

            return View(nomesAnimaisFiltrados.ToList());
        }

        [HttpPost]
        public IActionResult ListarRaca(string filtroRaca)
        {
            var racasAnimaisFiltrados = from b in _context.Animal.Include(a => a.Raca) select b;

            if (!String.IsNullOrEmpty(filtroRaca))
            {
                racasAnimaisFiltrados = racasAnimaisFiltrados.Where(b => b.Raca.Designacao.Contains(filtroRaca));
            }

            return View(racasAnimaisFiltrados.ToList());
        }

        [HttpPost]
        public IActionResult ListarGenero(string filtroGenero)
        {
            var generoAnimaisFiltrados = from c in _context.Animal.Include(a => a.Raca) select c;

            if (!String.IsNullOrEmpty(filtroGenero))
            {
                generoAnimaisFiltrados = generoAnimaisFiltrados.Where(c => c.Genero.Contains(filtroGenero));
            }

            return View(generoAnimaisFiltrados.ToList());
        }

        [HttpPost]
        public IActionResult ListarCor(string filtroCor)
        {
            var corAnimaisFiltrados = from d in _context.Animal.Include(a => a.Raca) select d;

            if (!String.IsNullOrEmpty(filtroCor))
            {
                corAnimaisFiltrados = corAnimaisFiltrados.Where(d => d.Cor.Contains(filtroCor));
            }

            return View(corAnimaisFiltrados.ToList());
        }

        // GET: Animals
        public IActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DateSortParm = sortOrder == "date_desc" ? "Date" : "date_desc";

            var AnimaisArquivados = from d in _context.Animal.Include(a => a.Raca) select d;

            switch (sortOrder) //OrderBy para ascendente OU OrderByDescending para descendente
            {
                case "name_desc":
                    AnimaisArquivados = AnimaisArquivados.Where(d => d.Arquivado == false).OrderByDescending(s => s.Nome);
                    //AnimaisArquivados = AnimaisArquivados.OrderByDescending(s => s.Nome);
                    break;
                case "Date":
                    AnimaisArquivados = AnimaisArquivados.Where(d => d.Arquivado == false).OrderBy(s => s.DataNascimento);
                    break;
                case "date_desc":
                    AnimaisArquivados = AnimaisArquivados.Where(d => d.Arquivado == false).OrderByDescending(s => s.DataNascimento);
                    break;
                default: //data de entrada descendente
                    AnimaisArquivados = AnimaisArquivados.Where(d => d.Arquivado == false).OrderByDescending(s => s.DataEntrada);
                    break;
            }


            //AnimaisArquivados = AnimaisArquivados.Where(d => d.Arquivado == false);

            return View(AnimaisArquivados.ToList());

        }

        [Authorize(Roles = "Administrator")]
        public IActionResult IndexArquivo()
        {
            var AnimaisArquivados = from d in _context.Animal.Include(a => a.Raca) select d;

            AnimaisArquivados = AnimaisArquivados.Where(d => d.Arquivado == true);

            return View(AnimaisArquivados.ToList());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.Raca)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        /// <summary>
        /// Método chamado aquando da operação GET para a criação de um Animal
        /// </summary>
        /// <returns>
        /// Retorna uma resposta HTTP 302 para o browser chamando a ação Index do controller Manager
        /// </returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["RacaId"] = new SelectList(_context.Set<Raca>(), "RacaId", "Designacao");
            return View();
        }

        // POST: Animals/Create
        /// <summary>
        /// Método chamado aquando da operação POST para a criação de um animal. Recebe como
        /// parametros o modelo relativo a um animal, um inteiro que representa o id do animal,
        /// um inteiro que representa o id da raça, uma string que representa o genero,
        /// uma string que representa uma cor, datas de nascimento, entrada e ultima vacina e uma string que representa a fotografia do animal.
        /// </summary>
        /// <returns>
        /// Retorna a view correspondente passando como parametro o model de um Animal.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,RacaId,Nome,Genero,Cor,DataNascimento,DataEntrada,DataVacina,FicheiroFoto")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                animal.Arquivado = false;
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RacaId"] = new SelectList(_context.Set<Raca>(), "RacaId", "Designacao", animal.RacaId);
            return View(animal);
        }

        // GET: Animals/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["RacaId"] = new SelectList(_context.Set<Raca>(), "RacaId", "Designacao", animal.RacaId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,RacaId,Nome,Genero,Cor,DataNascimento,DataEntrada,DataVacina,FicheiroFoto")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
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
            ViewData["RacaId"] = new SelectList(_context.Set<Raca>(), "RacaId", "Designacao", animal.RacaId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.Raca)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            //_context.Animal.Remove(animal);
            animal.Arquivado = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.AnimalId == id);
        }



        public IActionResult AnimaisPDF()
        {

            return new ViewAsPdf("Index", _context.Animal.ToList());
        }




    }
}