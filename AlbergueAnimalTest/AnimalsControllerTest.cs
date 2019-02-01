using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbergueAnimal.Controllers;
using AlbergueAnimal.Data;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AlbergueAnimalTest
{
    public class AnimalsControllerTest
    {
        [Fact]
        public void Index_CanLoadFromContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                context.Raca.AddRange(
                    new Raca { Designacao = "Labrador" },
                    new Raca { Designacao = "Pug" },
                    new Raca { Designacao = "Husky" });
                context.SaveChanges();

                context.Animal.AddRange(
                    new Animal { RacaId = 1, Nome = "Bobby", Genero = "Macho", Cor = "Preto", DataEntrada = new DateTime(1998, 04, 30), DataVacina = new DateTime(1998, 04, 30), DataNascimento = new DateTime(1998, 03, 30), Arquivado = false },
                    new Animal { RacaId = 1, Nome = "Bobby", Genero = "Macho", Cor = "Preto", DataEntrada = new DateTime(1998, 04, 30), DataVacina = new DateTime(1998, 04, 30), DataNascimento = new DateTime(1998, 03, 30), Arquivado = false },
                    new Animal { RacaId = 1, Nome = "Bobby", Genero = "Macho", Cor = "Preto", DataEntrada = new DateTime(1998, 04, 30), DataVacina = new DateTime(1998, 04, 30), DataNascimento = new DateTime(1998, 03, 30), Arquivado = false });
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AnimalsController(context);

                var result = controller.Index("", "");

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Animal>>(
                    viewResult.ViewData.Model);
                Assert.Equal(3, model.Count());
            }
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenAnimalDoesntExist()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AnimalsController(context);

                var result = await controller.Details(1);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AnimalsController(context);

                var result = await controller.Details(null);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task Details_RetunrsViewResult_WhenAnimalExists()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                context.Raca.Add(new Raca { RacaId = 5, Designacao = "Labrador" });
                context.Animal.Add(new Animal { RacaId = 5, Nome = "Bobby", Genero = "Macho", Cor = "Preto", DataEntrada = new DateTime(1998, 04, 30), DataVacina = new DateTime(1998, 04, 30), DataNascimento = new DateTime(1998, 03, 30), Arquivado = false });
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AnimalsController(context);

                var result = await controller.Details(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<Animal>(viewResult.ViewData.Model);
                Assert.Equal(1, model.AnimalId);
            }
        }
    }
}
