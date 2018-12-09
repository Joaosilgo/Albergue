
using AlbergueAnimal.Data;

using AlbergueAnimal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlbergueAnimal.Models
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Raca.Any())
            {

                context.Raca.Add(new Raca { Designacao = "Labradore" });
                context.Raca.Add(new Raca { Designacao = "PitBull" });
                context.Raca.Add(new Raca { Designacao = "Salsicha" });
                context.Raca.Add(new Raca { Designacao = "Dalmata" });

                context.SaveChanges();
            }

            if (!context.Animal.Any())
            {

                context.Animal.Add(new Animal { RacaId = 1, Nome = "Cao1", Genero = "M", Cor = "Preto", DataEntrada = new DateTime(1998, 04, 30), DataVacina = new DateTime(1998, 04, 30), DataNascimento = new DateTime(1998, 04, 30), FicheiroFoto = "pic1" });

                context.SaveChanges();


            }
        }
    }
}



