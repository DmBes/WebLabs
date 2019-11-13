using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Extensions;
using ASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Components
{
    public class CartViewComponent : ViewComponent
    {
        private Cart _cart;

        public CartViewComponent(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
           // var cart = HttpContext.Session.Get<Cart>("cart") 
             //          ?? new Cart();
            return View(_cart);
        }
    }
}
