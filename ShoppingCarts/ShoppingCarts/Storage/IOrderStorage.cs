using ShoppingCarts.Helpers;
using ShoppingCarts.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCarts.Storage
{
    public interface IOrderStorage
    {
        Task<TryResult<Order>> CreateAsync(Order order, CancellationToken cancellationToken = default);
    }
}
