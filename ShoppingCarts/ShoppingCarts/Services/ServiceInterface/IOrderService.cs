using ShoppingCarts.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCarts.Services.ServiceInterface
{
    public interface IOrderService
    {
        Task CreateAsync(User user, string train, int carriage, int place, IReadOnlyDictionary<Item, int> cart);
    }
}
