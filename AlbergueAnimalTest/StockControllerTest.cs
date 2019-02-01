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
    public class StockControllerTest
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
                context.ProductType.AddRange(
                    new ProductType { Nome = "Alimento" },
                    new ProductType { Nome = "Educação" },
                    new ProductType { Nome = "Bem-Estar" });
                context.SaveChanges();

                context.Product.AddRange(
                    new Product { ProductTypeID = 1, Nome = "Osso", Referencia = "PR12345", Preco = 5.0, Quantidade = 10, QuantidadeLimite = 5 },
                    new Product { ProductTypeID = 1, Nome = "Osso", Referencia = "PR12345", Preco = 5.0, Quantidade = 10, QuantidadeLimite = 5 },
                    new Product { ProductTypeID = 1, Nome = "Osso", Referencia = "PR12345", Preco = 5.0, Quantidade = 10, QuantidadeLimite = 5 });
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new ProductTypeController(context);

                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<ProductType>>(
                    viewResult.ViewData.Model);
                Assert.Equal(3, model.Count());
            }
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenProductDoesntExist()
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
                var controller = new StockController(context);

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
                var controller = new StockController(context);

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
                context.ProductType.Add(new ProductType { ProductTypeID = 5, Nome = "Alimento" });
                context.Product.Add(new Product { ProductTypeID = 1, Nome = "Osso", Referencia = "PR54321", Preco = 5.0, Quantidade = 15, QuantidadeLimite = 5 });
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AnimalsController(context);

                var result = await controller.Details(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
                Assert.Equal(1, model.ProductID);
            }
        }
    }
}
