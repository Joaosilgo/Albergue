using AlbergueAnimal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    public class Animal
    {
        // vai ser chave primária
        public int AnimalId { get; set; }

        // vai ser chave estrangeira
        [Display(Name = "Raca")]
        public int RacaId { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório"), StringLength(15)]
        public String Nome { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "O Genero é obrigatório")]
        public String Genero { get; set; }

        [Display(Name = "Cor")]
        [Required(ErrorMessage = "A Cor é obrigatório")]
        public String Cor { get; set; }

        [Required]
        [Display(Name = "Data De Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name = "Data De Entrada")]
        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; }

        [Required]
        [Display(Name = "Data De Vacina")]
        [DataType(DataType.Date)]
        public DateTime DataVacina { get; set; }


        [StringLength(255)]
        [Display(Name = "Fotografia")]
        public string FicheiroFoto { get; set; }


        // propriedade navigacional
        public virtual Raca Raca { get; set; }

    }
}
