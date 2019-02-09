using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlbergueAnimal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlbergueAnimal.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context);
        }
       
        public IActionResult TopVisualizacoesAnimal()
        {
            return View(_context.Animal.Include(p=> p.Raca).Where(a => a.Arquivado == false && a.visualizacoes > 0).ToList());
        }
    }
}