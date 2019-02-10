using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    /// <summary>
    /// Classe que representa um produto.
    /// </summary>
    /// <remarks>
    /// Representada através dos atributos ProductID, ProductTypeID, Nome, Referencia, Preco, Quantidade e QuantidadeLimite.
    /// </remarks>
    public class Product
    {
        // vai ser chave primária
        [Key]
        public int ProductID { get; set; }

        // vai ser chave estrangeira
        /// <summary>Propriedade ProductTypeID representa o tipo de produto.</summary>
        /// <value>Permite o get e o set desta propriedade. Vai buscar o nome do Producto ao model ProductType.</value>
        [Display(Name = "Tipo de Producto")]
        public int ProductTypeID { get; set; }

        /// <summary>Propriedade Nome representa o nome do produto.</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados e deverá conter no maximo 15 caracteres.</value>
        [Required(ErrorMessage = "O Nome é obrigatório"), StringLength(15)]
        public String Nome { get; set; }

        /// <summary>Propriedade Referencia representa a referencia do produto .</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Display(Name = "Referência")]
        [Required(ErrorMessage = "A Referência é obrigatório")]
        public String Referencia { get; set; }

        /// <summary>Propriedade Preco representa a referencia do produto .</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O Preço é obrigatório")]
        public double Preco { get; set; }

        /// <summary>Propriedade Quantidade representa a quantidade do produto .</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "A Quantidade é obrigatório")]
        public int Quantidade { get; set; }

        /// <summary>Propriedade QuantidadeLimite representa o limite de quantidade do produto .</summary>
        /// <value>Permite o get e o set desta propriedade. Não poderá ser null na base de dados.</value>
        [Display(Name = "Limite de Quantidade")]
        public int QuantidadeLimite { get; set; }

        /// <summary>Propriedade imageContent representa o conteudo da imagem do produto.</summary>
        /// <value>Permite o get e o set desta propriedade.</value>
        [ScaffoldColumn(false)]
        public byte[] imageContent { get; set; }

        /// <summary>Propriedade imageMimeType representa o tipo da imagem do produto.</summary>
        /// <value>Permite o get e o set desta propriedade.</value>
        [StringLength(256)]
        [ScaffoldColumn(false)]
        public String imageMimeType { get; set; }

        /// <summary>Propriedade imageFileName representa o nome da imagem do produto.</summary>
        /// <value>Permite o get e o set desta propriedade.</value>
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
