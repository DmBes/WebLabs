using System;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ASP.Controllers;
using ASP.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using ASP.Models;

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
                new Boots {BootsId = 1,BootsGroupId= 1},
                new Boots {BootsId = 2,BootsGroupId= 1},
                new Boots {BootsId = 3,BootsGroupId= 2},
                new Boots {BootsId = 4, BootsGroupId= 3 },
                new Boots {BootsId = 5, BootsGroupId= 4},
                new Boots {BootsId = 4, BootsGroupId= 3 },
                new Boots {BootsId = 5, BootsGroupId= 4},
                new Boots {BootsId = 4, BootsGroupId= 3 },
                new Boots {BootsId = 5, BootsGroupId= 4}
            };
            // Act 
            var result = controller.Index(pageNo: page, group: null) as ViewResult ;
            var model = result?.Model as List<Boots>;


            // Assert
            Assert.NotNull(model);
            Assert.Equal(qty, model.Count);
            Assert.Equal(id, model[0].BootsId);
        }




        /// <summary>
        /// Исходные данные для теста
        /// номер страницы, кол.объектов на выбранной странице и
        /// id первого объекта на странице
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { 1, 3, 1 };
            yield return new object[] { 2,2,4 };
        } 

        /// <summary>
        /// Получение тестового списка объектов
        /// </summary>
        /// <returns></returns>
        private List<Boots> GetBootsList()
        {             return new List<Boots>
        {
            new Boots{ BootsId= 1},
            new Boots{ BootsId=2},
            new Boots{ BootsId=3}, 
            new Boots{ BootsId=4}, 
            new Boots{ BootsId=5}

        };
        } 

        



        [Theory]
        [MemberData(memberName: nameof(Data))]
        public void ListViewModelCountsPages(int page, int qty, int id)
        { // Act
          var model = ListViewModel<Boots>.GetModel(GetBootsList(), page, 3); 
            // Assert
            Assert.Equal(2, model.TotalPages);             
        }

        [Theory]
        [MemberData(memberName: nameof(Data))]
        public void ListViewModelSelectsCorrectQty(int page, int qty, int id)
        { // Act
          var model = ListViewModel<Boots>.GetModel(GetBootsList(), page, 3); 
            // Assert
            Assert.Equal(qty, model.Count); 
        }
        [Theory]
        [MemberData(memberName: nameof(Data))]
        public void ListViewModelHasCorrectData(int page, int qty, int id)
        { // Act
          var model = ListViewModel<Boots>.GetModel(GetBootsList(), page, 3); 
            // Assert
            Assert.Equal(id, model[0].BootsId); 
        }

        [Fact]
        public void ControllerSelectsGroup()
        { // arrange
          var controller = new ProductController(); 
          controller._boots = GetBootsList();
          // act
          var result = controller.Index(4) as ViewResult;
            var model = result.Model as List<Boots>;
            
            // assert

            Assert.Equal(1, model.Count);
            Assert.Equal(GetBootsList()[3],  model[0],  Comparer<Boots>.GetComparer((d1,d2)=>
                { return d1.BootsId == d2.BootsId; })); }
        }
}
