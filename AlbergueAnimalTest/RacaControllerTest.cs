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
    public class RacaControllerTest
    {
        [Fact]
        public async Task Index_CanLoadFromContext()
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
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new RacaController(context);

                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Raca>>(
                    viewResult.ViewData.Model);
                Assert.Equal(3, model.Count());
            }
        }
    }
}
