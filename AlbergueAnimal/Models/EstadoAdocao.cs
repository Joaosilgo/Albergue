using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    /// <summary>
    /// Classe que representa o estado de uma adoção.
    /// </summary>
    public class EstadoAdocao
    {
        [Display(Name = "Estado da Adoção")]
        public int EstadoAdocaoId { get; set; }

        /// <summary>Propriedade estado representa o estado da adoção.</summary>
        public string estado { get; set; }

        public virtual List<Adocao> Adocao { get; set; }
    }
}
