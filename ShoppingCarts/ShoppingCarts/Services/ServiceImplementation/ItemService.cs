using MongoDB.Bson;
using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using ShoppingCarts.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShoppingCarts.Services.ServiceImplementation.ItemService))]

namespace ShoppingCarts.Services.ServiceImplementation
{
    public class ItemService : IItemService
    {
        #region Fields
        private static IEnumerable<Item> items = null;
        private IItemStorage itemStorage;
        #endregion

        #region MyRegion
        public ItemService()
        {
            itemStorage = DependencyService.Get<IItemStorage>();
        }

        #endregion
        public async Task<Item> GetItemAsync(ObjectId id)
        {
            if (items == null)
                await GetItemsAsync();

            return await Task.FromResult(items.FirstOrDefault(i => i.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            if (items == null)
            {
                var result = await itemStorage.GetItemsAsync();
                if (!result.IsFaulted)
                {
                    items = result.Value.Where(i => i.Balance > 0);
                } else
                {
                    throw result.Exception;
                }
            }
            return items;
        }

        public async Task UpdateItemsAsync()
        {
            var result = await itemStorage.GetItemsAsync();
            if (!result.IsFaulted)
            {
                items = result.Value.Where(i => i.Balance > 0);
            } else
            {
                throw result.Exception;
            }
        }
    }
}