using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    public class EstadoAdocao
    {
        [Display(Name = "Estado da Adoção")]
        public int EstadoAdocaoId { get; set; }

        public string estado { get; set; }

        public virtual List<Adocao> Adocao { get; set; }
    }
}
