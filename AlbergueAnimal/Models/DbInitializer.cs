
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
                context.Raca.Add(new Raca { Designacao = "Labrador" });
                context.Raca.Add(new Raca { Designacao = "PitBull" });
                context.Raca.Add(new Raca { Designacao = "Salsicha" });
                context.Raca.Add(new Raca { Designacao = "Dálmata" });
                context.Raca.Add(new Raca { Designacao = "Beagle" });
                context.Raca.Add(new Raca { Designacao = "Basset Hound" });
                context.Raca.Add(new Raca { Designacao = "Pug" });
                context.Raca.Add(new Raca { Designacao = "Chihuahua" });
                context.Raca.Add(new Raca { Designacao = "Husky" });
                context.Raca.Add(new Raca { Designacao = "Pastor-Alemão" });

                context.SaveChanges();
            }

            if (!context.Animal.Any())
            {          
                context.Animal.Add(new Animal { RacaId = 1, Nome = "Bobby", Genero = "Macho", Cor = "Preto", DataEntrada = new DateTime(1998, 04, 30), DataVacina = new DateTime(1998, 04, 30), DataNascimento = new DateTime(1998, 04, 30), FicheiroFoto = "pic1", Arquivado = false });
                context.Animal.Add(new Animal { RacaId = 1, Nome = "Peludo", Genero = "Macho", Cor = "Castanho", DataEntrada = new DateTime(2015, 10, 22), DataVacina = new DateTime(2016, 05, 10), DataNascimento = new DateTime(2014, 09, 05), FicheiroFoto = "pic2", Arquivado = false });

                context.SaveChanges();
            }
        }
    }
}



