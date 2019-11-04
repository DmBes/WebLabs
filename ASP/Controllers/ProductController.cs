using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.DAL.Entities;
using ASP.Models;

namespace ASP.Controllers
{
    public class ProductController : Controller
    {
        public List<Boots> _boots;
        private List<BootsGroup> _bootsGroups;
        int _pageSize;


        public ProductController()
        {
            _pageSize = 3;
            SetupData();
        }



        public IActionResult Index(int? group, int pageNo = 1)
        {
            var items = _boots.Skip((pageNo - 1) * _pageSize).Take(_pageSize).ToList();
            var bootsFiltered = _boots.Where(d => !group.HasValue
                                                   || d.BootsId == group.Value).ToList();

            // Поместить список групп во ViewData 
            ViewData["Groups"] = _bootsGroups; 

            // Получить id текущей группы и поместить в TempData
            var currentGroup = 0;
            try
            {
                int.TryParse(HttpContext.Request.Query["group"], out currentGroup);
                TempData["CurrentGroup"] = currentGroup;
            }
            catch (NullReferenceException e)
            {

                currentGroup = 0;
            }



            return View(ListViewModel<Boots>.GetModel(bootsFiltered, pageNo, _pageSize));
        }






        /// <summary>
        /// Инициализация списков
        /// </summary> 

        private void SetupData()
        {
            _bootsGroups = new List<BootsGroup>
            {
                new BootsGroup {BootsGroupId= 1, GroupName="LeatherBoots"}, 
                new BootsGroup {BootsGroupId=2, GroupName="Ugg"},
                new BootsGroup {BootsGroupId=3, GroupName="Sneakers"},
                new BootsGroup {BootsGroupId=4, GroupName="Slippers"},
                new BootsGroup {BootsGroupId=5, GroupName="Galoshes"},
                new BootsGroup {BootsGroupId=6, GroupName="Macasiness"}

            };


            _boots = new List<Boots>
                { new Boots { BootsId = 1, BootsName = "Estella", Description = "for woman ", Size = 280, BootsGroupId = 1, Image = "9816213-1.jpg" }, 
                    new Boots { BootsId = 2, BootsName = "Adidas", Description = "sport", Size = 315, BootsGroupId = 3, Image = "snikkers.jpg" },
                    new Boots { BootsId = 3, BootsName = "DED", Description = "My predok", Size = 340, BootsGroupId = 5, Image = "galosesh.jpg" }, 
                    new Boots { BootsId = 4, BootsName = "House", Description = "comfortable slippers", Size = 275, BootsGroupId = 4, Image = "slippers.jpg" },
                    new Boots { BootsId = 5, BootsName = "CARLABEI", Description = "africa style", Size = 285, BootsGroupId = 6, Image = "macasins.jpg" } };
        }
    }
}
