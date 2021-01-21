using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using MongoDB.Bson;

[assembly: Dependency(typeof(ShoppingCarts.Services.ServiceImplementation.CartService))]

namespace ShoppingCarts.Services.ServiceImplementation
{
    public class CartService : ICartService
    {
        #region Fields
        private static Dictionary<Item, int> cart = new Dictionary<Item, int>();
        #endregion

        #region ICartService
        public void Add(Item item)
        {
            if (cart.TryGetValue(item, out var count))
            {
                count++;
                cart[item] = count;
            }
            else
            {
                cart.Add(item, 1);
            }
        }

        public bool RemoveById(ObjectId id)
        {
            var item = cart.Keys.FirstOrDefault(i => i.Id == id);
            if (item != null)
                return cart.Remove(item);
            return false;
        }

        public void Clear()
        {
            cart.Clear();
        }

        public IReadOnlyDictionary<Item, int> GetItems() => cart;
        #endregion
    }
}
