
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    public class Raca
    {

        public int RacaId { get; set; }

        [Display(Name = "Raça")]
        [Required(ErrorMessage = "A designação da Raça é obrigatória"), StringLength(20)]
        public String Designacao { get; set; }

        public virtual List<Animal> Animais { get; set; }

    }
}
