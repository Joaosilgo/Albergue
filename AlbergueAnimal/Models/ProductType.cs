using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    /// <summary>
    /// Classe que representa o tipo de um produto.
    /// </summary>
    public class ProductType
    {
        [Key]
        public int ProductTypeID { get; set; }

        /// <summary>Propriedade Nome representa o nome do produto.</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Display(Name = "Produto")]
        [Required(ErrorMessage = "A designação do Produto é obrigatória"), StringLength(20)]
        public String Nome { get; set; }

        public virtual List<Product> Produtos { get; set; }
    }
}
