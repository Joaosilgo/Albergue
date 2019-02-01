using AlbergueAnimal.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AlbergueAnimalTest
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            var controller = new HomeController();

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void About_ReturnsViewResult()
        {
            var controller = new HomeController();

            var result = controller.About();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void About_SetsMessageInViewData()
        {
            var controller = new HomeController();

            controller.About();

            Assert.Null(controller.ViewData["Message"]); //IsNull
        }

        [Fact]
        public void Contact_ReturnsViewResult()
        {
            var controller = new HomeController();

            var result = controller.Contact();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Contact_SetsMessageInViewData()
        {
            var controller = new HomeController();

            controller.Contact();

            Assert.Null(controller.ViewData["Message"]);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            var controller = new HomeController();

            var result = controller.Contact();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_SetsMessageInViewData()
        {
            var controller = new HomeController();

            controller.Contact();

            Assert.Null(controller.ViewData["Message"]);
        }
    }
}
