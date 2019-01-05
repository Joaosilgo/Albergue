using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    public class EstadoAdocao
    {
        public int EstadoAdocaoId { get; set; }

        public string estado { get; set; }

        public virtual List<Adocao> Adocao { get; set; }
    }
}
