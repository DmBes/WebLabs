using System;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ASP.Controllers;
using ASP.DAL.Data;
using ASP.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using ASP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;

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
            var controllerContex = new ControllerContext();

            // словарь для формирования значений Query
            var queryDictionary = new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>();
            queryDictionary.Add("group", "0");
            // макет для TempData
            var tempData = new Mock<ITempDataDictionary>();
            // объект для HttpContext
            var httpContext = new DefaultHttpContext();
            // поместить HttpContext в ControllerContext
            controllerContex.HttpContext = httpContext;
            // сформировать строку запроса
            controllerContex.HttpContext.Request.Query = new QueryCollection(queryDictionary);

            // настройка для контекста базы данных
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb").Options;
            // создать контекст
            using (var context = new ApplicationDbContext(options))
            {
                // заполнить контекст данными
                context.Bootses.AddRange(new List<Boots>
                {
                    new Boots {BootsId = 1},
                    new Boots {BootsId = 2},
                    new Boots {BootsId = 3},
                    new Boots {BootsId = 4},
                    new Boots {BootsId = 5}
                });
                context.BootsGroups.Add(new BootsGroup {GroupName = "fake group"});
                context.SaveChanges();
                // создать объект контроллера

                var controller = new ProductController(context) {ControllerContext = controllerContex};
                // сформировать объект TempData из макета

                controller.TempData = tempData.Object;

                // Act
                var result = controller.Index(pageNo: page, group: null) as ViewResult;
                var model = result?.Model as List<Boots>;

                // Assert
                Assert.NotNull(model);
                Assert.Equal(qty, model.Count);
                Assert.Equal(id, model[0].BootsId);
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
            }
        }




        //[Theory]
        //[MemberData(memberName: nameof(Data))]
        //public void ListViewModelCountsPages(int page, int qty, int id)
        //{ // Act
        //  var model = ListViewModel<Boots>.GetModel(GetBootsList(), page, 3); 
        //    // Assert
        //    Assert.Equal(2, model.TotalPages);             
        //}

        //[Theory]
        //[MemberData(memberName: nameof(Data))]
        //public void ListViewModelSelectsCorrectQty(int page, int qty, int id)
        //{ // Act
        //  var model = ListViewModel<Boots>.GetModel(GetBootsList(), page, 3); 
        //    // Assert
        //    Assert.Equal(qty, model.Count); 
        //}
        //[Theory]
        //[MemberData(memberName: nameof(Data))]
        //public void ListViewModelHasCorrectData(int page, int qty, int id)
        //{ // Act
        //  var model = ListViewModel<Boots>.GetModel(GetBootsList(), page, 3); 
        //    // Assert
        //    Assert.Equal(id, model[0].BootsId); 
        //}

        //[Fact]
        //public void ControllerSelectsGroup()
        //{ // arrange
        //  var controller = new ProductController(); 
        //  controller._boots = GetBootsList();
        //  // act
        //  var result = controller.Index(4) as ViewResult;
        //    var model = result.Model as List<Boots>;

        //    // assert

        //    Assert.Equal(1, model.Count);
        //    Assert.Equal(GetBootsList()[3],  model[0],  Comparer<Boots>.GetComparer((d1,d2)=>
        //        { return d1.BootsId == d2.BootsId; })); }
        //}
    }
}
