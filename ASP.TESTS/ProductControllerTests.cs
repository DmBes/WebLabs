using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ASP.Controllers;
using ASP.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ASP.TESTS
{
    public class ProductControllerTests
    {
        [Theory]
        [InlineData(1, 3, 1)] // 1-я страница, кол. объектов 3, id первого объекта 1
        [InlineData(2, 2, 4)] // 2-я страница, кол. объектов 2, id первого объекта 4
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            var controller = new ProductController();

            controller._boots = new List<Boots>
            {
                new Boots {BootsId = 1},
                new Boots {BootsId = 2},
                new Boots {BootsId = 3},
                new Boots {BootsId = 4},
                new Boots {BootsId = 5}
            };
            // Act 
            var result = controller.Index(page) as ViewResult;
            var model = result?.Model as List<Boots>;


            // Assert
            Assert.NotNull(model);
            Assert.Equal(qty, model.Count);
            Assert.Equal(id, model[0].BootsId);
        }
    }
}
