using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.DAL.Entities;

namespace ASP.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }

        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }   
        /// <summary>
        ///Количество объектов в корзине
        /// </summary>
        public int Count 
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
}

        /// <summary>
        /// Количество калорий
        /// </summary>
        public int Calories
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Boots.Size);
            }
        }                 
        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="boots">добавляемый объект</param>
        virtual public void AddToCart(Boots boots)
        { 
            // если объект есть в корзине
            // то увеличить количество
            if (Items.ContainsKey(boots.BootsId)) Items[boots.BootsId].Quantity++; 

            // иначе - добавить объект в корзину
            else Items.Add(boots.BootsId, new CartItem{Boots   = boots, Quantity = 1});

        }

        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id">id удаляемого объекта</param>
        virtual public void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }

        /// <summary>
        /// Очистить корзину
        /// </summary>
        virtual public void ClearAll()
        {
            Items.Clear();
        }

    } 



        /// <summary> 
        /// Клас описывает одну позицию в корзине
        /// </summary>
        public class CartItem
        {
            public Boots Boots { get; set; }
            public int Quantity { get; set; }
        } 
}
