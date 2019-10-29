using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.DAL.Entities;

namespace ASP.Controllers
{
    public class ProductController : Controller
    {
        private List<Boots> _boots;
        private List<BootsGroup> _bootsGroups;

        public ProductController()
        {
            SetupData();
        }



        public IActionResult Index()
        {
            return View(_boots);
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
