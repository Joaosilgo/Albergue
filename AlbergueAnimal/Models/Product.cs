using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    public class Product
    {
        // vai ser chave primária
        [Key]
        public int ProductID { get; set; }

        // vai ser chave estrangeira
        /// <summary>Propriedade ProductTypeID representa o tipo de producto do .</summary>
        /// <value>Permite o get e o set desta propriedade. Vai buscar o nome do Producto ao model ProductType.</value>
        [Display(Name = "Tipo de Producto")]
        public int ProductTypeID { get; set; }

        /// <summary>Propriedade Nome representa o nome do Producto para adoção.</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados e deverá conter no maximo 15 caracteres.</value>
        [Required(ErrorMessage = "O Nome é obrigatório"), StringLength(15)]
        public String Nome { get; set; }

        /// <summary>Propriedade Referencia representa o Referencia do Producto .</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Display(Name = "Referência")]
        [Required(ErrorMessage = "A Referência é obrigatório")]
        public String Referencia { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O Preço é obrigatório")]
        public double Preco { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "A Quantidade é obrigatório")]
        public int Quantidade { get; set; }

        [Display(Name = "Limite de Quantidade")]
        public int QuantidadeLimite { get; set; }

        [ScaffoldColumn(false)]
        public byte[] imageContent { get; set; }

        [StringLength(256)]
        [ScaffoldColumn(false)]
        public String imageMimeType { get; set; }

        [StringLength(100, ErrorMessage = "O nome do ficheiro não pode ser mostrado")]
        [Display(Name = "Nome do Ficheiro")]
        [ScaffoldColumn(false)]
        public String imageFileName { get; set; }

        //propriedade navigacional
        /// <summary>Propriedade ProductType representa a o tipo de produto .</summary>
        /// <value>Permite o get e o set desta propriedade. Vai buscar o nome do Producto ao model ProductType.</value>
        [Display(Name = "Tipo de Producto")]
        public virtual ProductType ProductType { get; set; } //o que aparece na página


       

    }
}
