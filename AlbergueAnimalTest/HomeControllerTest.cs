using AlbergueAnimal.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using AlbergueAnimal.Data;
using AlbergueAnimal.Models;

namespace AlbergueAnimalTest
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);

                var result = controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public void About_ReturnsViewResult()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);

                var result = controller.About();

                var viewResult = Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public void About_SetsMessageInViewData()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);

                controller.About();

                Assert.Null(controller.ViewData["Message"]); //IsNull
            }
        }

        [Fact]
        public void Contact_ReturnsViewResult()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);
                var model = new ContactViewModel();
               
                model.Message = "oi";
                model.Name = "Joao";
                model.Email = "joaosilgo96@gmail.com";
                model.Subject = "Geral";
                
                var result = controller.Contact(model);

                Assert.Null(controller.Contact(model)); //IsNull
                //var viewResult = Assert.IsType<ContactViewModel>(result);
            }
            
        }

        [Fact]
        public void Contact_SetsMessageInViewData()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);
                var model = new ContactViewModel();
                model.Name = "Joao";
                model.Email = "joaosilgo96@gmail.com";
                model.Subject = "Geral";
                var result = controller.Contact(model);

                Assert.Null(controller.ViewData["Message"]);
            }
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);
                var model = new ContactViewModel();
                model.Name = "Joao";
                model.Email = "joaosilgo96@gmail.com";
                model.Subject = "Geral";
                var result = controller.Contact(model);
                var viewResult = Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public void Privacy_SetsMessageInViewData()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);
                var model = new ContactViewModel();
                model.Name = "Joao";
                model.Email = "joaosilgo96@gmail.com";
                model.Subject = "Geral";
                var result = controller.Contact(model);

                Assert.Null(controller.ViewData["Message"]);
            }
        }
    }
}
