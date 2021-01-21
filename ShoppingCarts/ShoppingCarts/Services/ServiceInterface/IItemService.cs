using MongoDB.Bson;
using ShoppingCarts.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCarts.Services.ServiceInterface
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemAsync(ObjectId id);
        Task UpdateItemsAsync();
    }
}