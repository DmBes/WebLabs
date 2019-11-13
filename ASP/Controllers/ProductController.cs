using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using ASP.DAL.Entities;
using ASP.Extensions;
using ASP.Models;
using Microsoft.Extensions.Logging;

namespace ASP.Controllers
{
    public class ProductController : Controller
    {
        private ILogger _logger;
        ApplicationDbContext _context;


        //public List<Boots> _boots;
        //private List<BootsGroup> _bootsGroups;
        int _pageSize;


        public ProductController(ApplicationDbContext context)
        {
            _pageSize = 3;
            _context = context;
        }


        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1)
        {
            var items = _context.Bootses.Skip((pageNo - 1) * _pageSize).Take(_pageSize).ToList();
            var bootsFiltered = _context.Bootses.Where(d => !group.HasValue
                                                   || d.BootsId == group.Value).ToList();

            // Поместить список групп во ViewData 
            ViewData["Groups"] = _context.BootsGroups; 

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

            if (Request.IsAjaxRequest()) 
                return PartialView("_ListPartial", ListViewModel<Boots>.GetModel(bootsFiltered, pageNo, _pageSize));
            return View(ListViewModel<Boots>.GetModel(bootsFiltered, pageNo, _pageSize));

            //if (Request.Headers["x-requested-with"] == "XMLHttpRequest")
            //    return PartialView("_ListPartial", ListViewModel<Boots>.GetModel(bootsFiltered, pageNo, _pageSize));
            //return View(ListViewModel<Boots>.GetModel(bootsFiltered, pageNo, _pageSize));

            // return View(ListViewModel<Boots>.GetModel(bootsFiltered, pageNo, _pageSize));
        }






        /// <summary>
        /// Инициализация списков
        /// </summary> 

        //private void SetupData()
        //{
        //    _bootsGroups = new List<BootsGroup>
        //        {
        //            new BootsGroup {BootsGroupId= 1, GroupName="LeatherBoots"},
        //            new BootsGroup {BootsGroupId=2, GroupName="Ugg"},
        //            new BootsGroup {BootsGroupId=3, GroupName="Sneakers"},
        //            new BootsGroup {BootsGroupId=4, GroupName="Slippers"},
        //            new BootsGroup {BootsGroupId=5, GroupName="Galoshes"},
        //            new BootsGroup {BootsGroupId=6, GroupName="Macasiness"}

        //        };


        //    _boots = new List<Boots>
        //            { new Boots { BootsId = 1, BootsName = "Estella", Description = "for woman ", Size = 280, BootsGroupId = 1, Image = "9816213-1.jpg" },
        //                new Boots { BootsId = 2, BootsName = "Adidas", Description = "sport", Size = 315, BootsGroupId = 3, Image = "snikkers.jpg" },
        //                new Boots { BootsId = 3, BootsName = "DED", Description = "My predok", Size = 340, BootsGroupId = 5, Image = "galosesh.jpg" },
        //                new Boots { BootsId = 4, BootsName = "House", Description = "comfortable slippers", Size = 275, BootsGroupId = 4, Image = "slippers.jpg" },
        //                new Boots { BootsId = 5, BootsName = "CARLABEI", Description = "africa style", Size = 285, BootsGroupId = 6, Image = "macasins.jpg" } };
        //}
    }
}
