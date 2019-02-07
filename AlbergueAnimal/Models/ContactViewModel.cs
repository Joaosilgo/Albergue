using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Por Favor insira o seu Nome")]
        [StringLength(20, MinimumLength = 0)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Por Favor insira um Email válido")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Selecione um assunto")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Fale Connosco")]
        public string Message { get; set; }
    }
}
