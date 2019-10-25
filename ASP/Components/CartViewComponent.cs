using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() 
        { return View(); }
    }
}
