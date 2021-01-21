using ShoppingCarts.Model;
using System.Collections.Generic;

namespace ShoppingCarts.Translators
{
    public static class ItemTranslator
    {
        public static CartItemModel ToCartItemModel(this Item item, IReadOnlyDictionary<Item, int> cart)
        {
            return new CartItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Image = item.Image,
                Description = item.Description,
                ShortDescription = item.ShortDescription,
                Price = item.Price,
                IsInCart = cart.ContainsKey(item)
            };            
        }
    }
}
