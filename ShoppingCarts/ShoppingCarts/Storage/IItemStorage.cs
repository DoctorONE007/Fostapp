using MongoDB.Bson;
using ShoppingCarts.Helpers;
using ShoppingCarts.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCarts.Storage
{
    public interface IItemStorage
    {
        Task<TryResult<IEnumerable<Item>>> GetItemsAsync(CancellationToken cancellationToken = default);
        Task<TryResult<bool>> ChangeItemAsync(ObjectId id, int balance, CancellationToken cancellationToken = default);
    }
}
