﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AlbergueAnimal.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace AlbergueAnimal.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Utilizador> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;

        public LoginModel(SignInManager<Utilizador> signInManager, ILogger<LoginModel> logger/*, ApplicationDbContext context*/)
        {
            _signInManager = signInManager;
            _logger = logger;
            //_context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O email não está inserido")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "A password não está inserida")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Lembrar-me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Login inválido.");
                    return Page();
                }
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                //_context.Users.Contains(Input.Email)

                if (result.Succeeded)
                {
                    //services.AddCaching();
                    ////if the list exists, add this user to it
                    //if (HttpRuntime.Cache["LoggedInUsers"] != null)
                    //{
                    //    //get the list of logged in users from the cache
                    //    var loggedInUsers = (Dictionary<string, DateTime>)HttpRuntime.Cache["LoggedInUsers"];

                    //    if (!loggedInUsers.ContainsKey(model.Email))
                    //    {
                    //        //add this user to the list
                    //        loggedInUsers.Add(Model.Email, DateTime.Now);
                    //        //add the list back into the cache
                    //        HttpRuntime.Cache["LoggedInUsers"] = loggedInUsers;
                    //    }
                    //}

                    ////the list does not exist so create it
                    //else
                    //{
                    //    //create a new list
                    //    var loggedInUsers = new Dictionary<string, DateTime>();
                    //    //add this user to the list
                    //    loggedInUsers.Add(model.Email, DateTime.Now);
                    //    //add the list into the cache
                    //    HttpRuntime.Cache["LoggedInUsers"] = loggedInUsers;
                    //}

                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Conta bloqueada.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login inválido.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
