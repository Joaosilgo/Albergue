using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    public class Utilizador : IdentityUser
    {

        [Required]
        [PersonalData]
        public string Nome { get; set; }

        [Required]
        [PersonalData]
        public DateTime DBO { get; set; }

        [Required]
        [PersonalData]
        public string Morada { get; set; }

        [Required]
        [PersonalData]
        public string Genero { get; set; }

        [StringLength(255)]
        [PersonalData]
        public string FicheiroFoto { get; set; }
    }
}
