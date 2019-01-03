﻿using System;
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

        // GET: Animals
        public IActionResult Index(string sortOrder, string searchString, int page = 0)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DateSortParm = sortOrder == "date_desc" ? "Date" : "date_desc";

            var AnimaisArquivados = from d in _context.Animal.Include(a => a.Raca) select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                AnimaisArquivados = AnimaisArquivados.Where(s => s.Nome.Contains(searchString) || s.Cor.Contains(searchString)
                || s.Genero.Contains(searchString) || s.Raca.Designacao.Contains(searchString));
            }

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

            const int PageSize = 3; // you can always do something more elegant to set this

            var count = AnimaisArquivados.Count();

            var data = AnimaisArquivados.Skip(page * PageSize).Take(PageSize).ToList();

            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

            this.ViewBag.Page = page;

            return this.View(data);

            //


            //return View(AnimaisArquivados.ToList());

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