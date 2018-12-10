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
        [Display(Name = "Raça")]
        public int RacaId { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório"), StringLength(15)]
        public String Nome { get; set; }

        [Display(Name = "Género")]
        [Required(ErrorMessage = "O Genero é obrigatório")]
        public String Genero { get; set; }

        [Display(Name = "Cor")]
        [Required(ErrorMessage = "A Cor é obrigatória")]
        public String Cor { get; set; }

        [Required(ErrorMessage = "A data de nasciment é obrigatória")]
        [Display(Name = "Data De Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "A data de entrada é obrigatória")]
        [Display(Name = "Data De Entrada")]
        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; }

        [Required(ErrorMessage = "A data da última vacina é obrigatória")]
        [Display(Name = "Data Última Vacina")]
        [DataType(DataType.Date)]
        public DateTime DataVacina { get; set; }


        [StringLength(255)]
        [Display(Name = "Fotografia")]
        public string FicheiroFoto { get; set; }


        //propriedade navigacional
        [Display(Name = "Raça")]
        public virtual Raca Raca { get; set; } //o que aparece na página

    }
}
