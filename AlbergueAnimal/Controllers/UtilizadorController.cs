using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlbergueAnimal.Data;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlbergueAnimal.Controllers
{
    public class UtilizadorController : Controller
    {
        private readonly ApplicationDbContext _context;// base dados applicação
        private readonly UserManager<Utilizador> _userManager;

        public UtilizadorController(ApplicationDbContext context, UserManager<Utilizador> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            return View(_context.Users.ToList());//lista users
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nome,DBO,Morada,Genero,Cargo")]  Utilizador utilizador, IFormFile profilePic)
        {
            // if (id != utilizador.Id)
            // {
            //     return NotFound();
            // }

            // //if (ModelState.IsValid)
            // //{
            //// try
            //// {
            //
            //  _context.Update(utilizador);
            //  var users=_context.Users.ToList();
            ////var user=  users.Where(u => u.Id.Equals(utilizador.Id));
            //var user = await _userManager.FindByIdAsync(utilizador.Id);
            ////    var currentRoles = new List<IdentityUserRole>();
            //var r = _context.Roles.ToList();//lista de roles existentes(Funcionario ,adaministradr,...)


            //var ur = _context.UserRoles.ToList();//lista de usersid e rolesid atribuidos
            //ur.Where(a => a.UserId.Equals(utilizador.Id));//lista so com utilizadores id;

            ////    currentRoles.AddRange(user.Roles);
            //foreach (var rel in r)//for de role names
            //{
            //    foreach (var role in ur)
            //    {
            //        if (role.UserId.Equals(utilizador.Id))
            //        {
            //            await _userManager.RemoveFromRoleAsync(utilizador, rel.Name);
            //        }
            //    }
            //}
            ////  _context.Update(utilizador);
            ////  var user =  await _context.Users.FindAsync(id);

            //// await _userManager.RemoveFromRoleAsync(user, "Administrator");
            //// Manager.AddToRole(user.Id, role);


            ////  _context.SaveChanges();


            //user.Cargo = utilizador.Cargo;
            ////   await _context.SaveChangesAsync();
            //await _userManager.AddToRoleAsync(user, utilizador.Cargo);
            ////}
            ////catch (DbUpdateConcurrencyException)
            ////{
            ////    if (!UtilizadorExists(utilizador.Id))
            ////    {
            ////        return NotFound();
            ////    }
            ////    else
            ////    {
            ////        throw;
            ////    }
            ////}
            var user = await _userManager.FindByIdAsync(utilizador.Id);
            if (profilePic != null)
            {
                string mimeType = profilePic.ContentType;
                long fileLength = profilePic.Length;
                if (!(mimeType == "" || fileLength == 0))
                {
                    if (mimeType.Contains("image"))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await profilePic.CopyToAsync(memoryStream);
                            utilizador.imageContent = memoryStream.ToArray();

                        }
                        utilizador.imageMimeType = mimeType;
                        utilizador.imageFileName = profilePic.FileName;
                    }
                }
            }

            // _context.Update(utilizador);
         //   await _userManager.UpdateSecurityStampAsync(utilizador);
            
          //  await _userManager.UpdateAsync(utilizador);

            //  await _signInManager.RefreshSignInAsync(utilizador);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            //}
            //return View(utilizador);
        }



        private bool UtilizadorExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }




        public async Task ClearUserRolesAsync(Utilizador utilizador)
        {
            var user = await _userManager.FindByIdAsync(utilizador.Id);
            //    var currentRoles = new List<IdentityUserRole>();
            var r = _context.Roles.ToList();//lista de roles existentes(Funcionario ,adaministradr,...)


            var ur = _context.UserRoles.ToList();//lista de usersid e rolesid atribuidos
            ur.Where(a => a.UserId.Equals(utilizador.Id));//lista so com utilizadores id;

            //    currentRoles.AddRange(user.Roles);
            foreach (var rel in r)//for de role names
            {
                foreach (var role in ur)
                {
                    if (role.UserId.Equals(utilizador.Id))
                    {
                        await _userManager.RemoveFromRoleAsync(utilizador, rel.Name);
                    }
                }
            }
        }





























        // GET: Raca/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }
















    }
}