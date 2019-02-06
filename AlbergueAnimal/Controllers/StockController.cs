using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlbergueAnimal.Data;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using AlbergueAnimal.Areas.Identity.Services;

namespace AlbergueAnimal.Controllers
{
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSenderStock _emailSender;


        public StockController(ApplicationDbContext context)
        {
            _context = context;
            _emailSender = new EmailSenderStock(context);
        }

        // GET: Stock
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.ProductType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stock/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Stock/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "Nome");
            return View();
        }

        // POST: Stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductTypeID,Nome,Referencia,Preco,Quantidade,QuantidadeLimite,imageContent,imageFileName,imageMimeType")] Product product, IFormFile thePicture)
        {
            if (ModelState.IsValid)
            {
                if (thePicture != null)
                {
                    string mimeType = thePicture.ContentType;
                    long fileLength = thePicture.Length;
                    if (!(mimeType == "" || fileLength == 0))
                    {
                        if (mimeType.Contains("image"))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await thePicture.CopyToAsync(memoryStream);
                                product.imageContent = memoryStream.ToArray();

                            }
                            product.imageMimeType = mimeType;
                            product.imageFileName = thePicture.FileName;
                        }

                    }
                }
                if (product.Quantidade < 1)
                {
                    ModelState.AddModelError("", "Por Favor Insira Uma Quantidade Válida");
                    ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "Nome", product.ProductTypeID);
                    return View(product);
                }
                if (product.Quantidade < product.QuantidadeLimite)
                {
                    ModelState.AddModelError("", "Quantidade não pode ser superior á Quantidade Limite" );
                    ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "Nome", product.ProductTypeID);
                    return View(product);
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "Nome", product.ProductTypeID);
            return View(product);
        }

        // GET: Stock/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "Nome", product.ProductTypeID);
            return View(product);
        }

        // POST: Stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductTypeID,Nome,Referencia,Preco,Quantidade,QuantidadeLimite")] Product product, IFormFile thePicture)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //começa aqui
                    if (thePicture == null)
                    {
                        product.imageContent = product.imageContent;
                        product.imageFileName = product.imageFileName;

                        product.imageMimeType = product.imageMimeType;
                    }
                    else
                    {
                        if (thePicture != null)
                        {
                            string mimeType = thePicture.ContentType;
                            long fileLength = thePicture.Length;
                            if (!(mimeType == "" || fileLength == 0))
                            {
                                if (mimeType.Contains("image"))
                                {
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        await thePicture.CopyToAsync(memoryStream);
                                        product.imageContent = memoryStream.ToArray();

                                    }
                                    product.imageMimeType = mimeType;
                                    product.imageFileName = thePicture.FileName;
                                }

                            }
                        }

                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
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
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "Nome", product.ProductTypeID);
            return View(product);
        }

        // GET: Stock/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductID == id);
        }




        public async Task<IActionResult> Requisitar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            if (product.Quantidade == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            product.Quantidade--;
            if (product.Quantidade <= product.QuantidadeLimite)
            {
                _emailSender.SendEmailAsync("Stock", "");
                _context.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            _context.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
