﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlbergueAnimal.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Utilizador> _userManager;
        private readonly SignInManager<Utilizador> _signInManager;
        private readonly IEmailSender _emailSender;

        public IndexModel(
            UserManager<Utilizador> userManager,
            SignInManager<Utilizador> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nome Completo")]
            public string Nome { get; set; }

            [Required]
            [Display(Name = "Data Nascimento")]
            [DataType(DataType.Date)]
            public DateTime DBO { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Morada")]
            [PersonalData]
            public string Morada { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Genero")]
            [PersonalData]
            public string Genero { get; set; }

            [StringLength(255)]
            [DataType(DataType.Text)]
            [Display(Name = "Fotografia")]
            [PersonalData]
            public string FicheiroFoto { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Telefone")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Nome = user.Nome,
                DBO = user.DBO,
                Morada = user.Morada,
                Genero = user.Genero,
                FicheiroFoto = user.FicheiroFoto,
                Email = email,
                PhoneNumber = phoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.Nome != user.Nome)
            {
                user.Nome = Input.Nome;
            }

            if (Input.DBO != user.DBO)
            {
                user.DBO = Input.DBO;
            }
            if (Input.Morada != user.Morada)
            {
                user.Morada = Input.Morada;
            }
            if (Input.Genero != user.Genero)
            {
                user.Genero = Input.Genero;
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }


            await _userManager.UpdateAsync(user);


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "O seu perfil foi atualizado";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirme o seu email",
                $"Por favor confirme o seu email <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

            StatusMessage = "Email de verificação enviado. Por favor verifique o seu email.";
            return RedirectToPage();
        }
    }
}