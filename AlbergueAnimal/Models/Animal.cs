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
        /// <summary>Propriedade RacaID representa a raça do animal para adoção.</summary>
        /// <value>Permite o get e o set desta propriedade. Vai buscar o nome da raça ao model Raca.</value>
        [Display(Name = "Raça")]
        public int RacaId { get; set; }

        /// <summary>Propriedade Nome representa o nome do animal para adoção.</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados e deverá conter no maximo 15 caracteres.</value>
        [Required(ErrorMessage = "O Nome é obrigatório"), StringLength(15)]
        public String Nome { get; set; }

        /// <summary>Propriedade Genero representa o género do animal para adoção.</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Display(Name = "Género")]
        [Required(ErrorMessage = "O Genero é obrigatório")]
        public String Genero { get; set; }

        /// <summary>Propriedade Cor representa a cor do animal para adoção.</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Display(Name = "Cor")]
        [Required(ErrorMessage = "A Cor é obrigatória")]
        public String Cor { get; set; }

        /// <summary>Propriedade DataNascimento representa a data de nascimento do animal para adoção.</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [Display(Name = "Data De Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        /// <summary>Propriedade DataEntrada representa a data de entrada do animal no canil.</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Required(ErrorMessage = "A data de entrada é obrigatória")]
        [Display(Name = "Data De Entrada")]
        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; }

        /// <summary>Propriedade DataVacina representa a data da ultima vacina do animal para adoção.</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Required(ErrorMessage = "A data da última vacina é obrigatória")]
        [Display(Name = "Data Última Vacina")]
        [DataType(DataType.Date)]
        public DateTime DataVacina { get; set; }

        /// <summary>Propriedade FicheiroFoto representa a imagem a do animal para adoção.</summary>
        /// <value>Permite o get e o set desta propriedade.</value>
        [StringLength(255)]
        [Display(Name = "Fotografia")]
        public string FicheiroFoto { get; set; }

        //propriedade navigacional
        /// <summary>Propriedade Raca representa a raça do animal para adoção.</summary>
        /// <value>Permite o get e o set desta propriedade. Vai buscar o nome da raça ao model Raca.</value>
        [Display(Name = "Raça")]
        public virtual Raca Raca { get; set; } //o que aparece na página

    }
}
