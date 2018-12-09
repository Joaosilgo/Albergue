using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;







namespace AlbergueAnimal.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Utilizador> _signInManager;
        private readonly UserManager<Utilizador> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<Utilizador> userManager,
            SignInManager<Utilizador> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {

            [Required(ErrorMessage = "O Nome não está preenchido.")]
            [DataType(DataType.Text)]
            [Display(Name = "Nome Completo")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "A Data de Nascimento não está preenchida")]
            [Display(Name = "Data Nascimento")]
            [DataType(DataType.Date)]
            public DateTime DBO { get; set; }

            [Required(ErrorMessage = "A Morada não está preenchida")]
            [DataType(DataType.Text)]
            [Display(Name = "Morada")]
            [PersonalData]
            public string Morada { get; set; }

            [Required(ErrorMessage = "O género não está selecionado")]
            [DataType(DataType.Text)]
            [Display(Name = "Genero")]
            [PersonalData]
            public string Genero { get; set; }

            [StringLength(255)]
            [DataType(DataType.Text)]
            [Display(Name = "Fotografia")]
            [PersonalData]
            public string FicheiroFoto { get; set; }


            [Required(ErrorMessage = "O email não está preenchido")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "A password não está preenchida")]
            [StringLength(100, ErrorMessage = "A {0} tem de ter no mínimo {2} e no máximo {1} Caracteres .", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar password")]
            [Compare("Password", ErrorMessage = "A password e a confirmação password não combinam.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new Utilizador
                {
                    
                    UserName = Input.Nome,
                    Email = Input.Email,
                    Nome = Input.Nome,
                    DBO = Input.DBO,
                    Morada = Input.Morada,
                    Genero = Input.Genero,
                    FicheiroFoto = Input.FicheiroFoto
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //  await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
