using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.DAL.Data;
using ASP.Extensions;
using ASP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart) 
        { _context = context;
            _cart = cart;
            //_cart = HttpContext.Session.Get<Cart>("cart");
        }
        public IActionResult Index()
        { //_cart = HttpContext.Session.Get<Cart>("cart");
            
            return View(_cart.Items.Values);
        }

        [Authorize]
        public IActionResult Add(int id, string returnUrl)
        {
            //_cart = HttpContext.Session.Get<Cart>("cart");
            var item = _context.Bootses.Find(id);
            if (item != null)
            {
                _cart.AddToCart(item);
                //HttpContext.Session.Set<Cart>("cart", _cart);
            } 
            return Redirect(returnUrl);
        }
        public IActionResult Delete(int id)
        {             //_cart = HttpContext.Session.Get<Cart>("cart");

            _cart.RemoveFromCart(id);
            //HttpContext.Session.Set<Cart>("cart", _cart);
            return RedirectToAction("Index");         }
        }
}