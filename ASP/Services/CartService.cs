using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.DAL.Entities;
using ASP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ASP.Extensions;
using Newtonsoft.Json;

namespace ASP.Services
{
    public class CartService : Cart
    {
        /// <summary>
        /// Объект сессии
        /// Не записывается в сессию вместе с CartService
        /// </summary>
         [JsonIgnore]
        public ISession Session;

        // переопределение методов класса Cart
        // для сохранения результатов в сессии
        public override void AddToCart(Boots boots)
        {
            base.AddToCart(boots);
            Session?.Set<CartService>("Cart", this);

        }

        public override void RemoveFromCart(int id)
        {
            base.RemoveFromCart(id);
            Session?.Set<CartService>("Cart", this);

        }

        public override void ClearAll()
        {
            base.ClearAll();
            Session?.Set<CartService>("Cart", this);

        }

        /// <summary>
        /// Получение объекта класса CartService
        /// </summary>
        /// <param name="serviceProvider">объект IserviceProvider</param>
        /// <returns> объекта класса CartService, приведенный к типу Cart</returns>
        /// TODO тут нужен статик???
        public static Cart GetCart(IServiceProvider serviceProvider)
        {
            // получение сессии
            var session = serviceProvider
                .GetRequiredService<IHttpContextAccessor>()?
                .HttpContext
                .Session;
            // получение объекта CartService из сессии
            // или создание нового объекта (для возможности тестирования)
            var cartService = session?.Get<CartService>("Cart")
                              ?? new CartService();
            cartService.Session = session;
            return cartService;

        }
    }
}

