using System;
using System.Collections.Generic;
using System.Text;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AlbergueAnimal.Data
{
    public class ApplicationDbContext : IdentityDbContext<Utilizador>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AlbergueAnimal.Models.Animal> Animal { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

        public DbSet<AlbergueAnimal.Models.Raca> Raca { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

        public DbSet<AlbergueAnimal.Models.EstadoAdocao> EstadoAdocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

        public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

      //  public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

      //  public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

        // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

        














        //public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

        //public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

       // public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

     //   public DbSet<AlbergueAnimal.Models.Adocao> Adocao { get; set; }

        //public DbSet<AlbergueAnimal.Models.Animal> ArquivoAnimal { get; set; }

      
    }
}
