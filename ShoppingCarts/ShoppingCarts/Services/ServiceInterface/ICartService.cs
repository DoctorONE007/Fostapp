using MongoDB.Bson;
using ShoppingCarts.Model;
using System.Collections.Generic;

namespace ShoppingCarts.Services.ServiceInterface
{
    public interface ICartService
    {
        void Add(Item item);
        bool RemoveById(ObjectId id);
        void Clear();
        IReadOnlyDictionary<Item, int> GetItems();
    }
}
