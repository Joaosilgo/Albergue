using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlbergueAnimal.Data;
using Microsoft.AspNetCore.Mvc;

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
    }
}