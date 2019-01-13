using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeID { get; set; }

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "A designação do Produto é obrigatória"), StringLength(20)]
        public String Nome { get; set; }

        public virtual List<Product> Produtos { get; set; }
    }
}
