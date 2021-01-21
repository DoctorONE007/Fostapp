using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using ShoppingCarts.Storage;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using MongoDB.Bson;

[assembly: Dependency(typeof(ShoppingCarts.Services.ServiceImplementation.OrderService))]
namespace ShoppingCarts.Services.ServiceImplementation
{
    public class OrderService : IOrderService
    {
        #region Fields
        private IOrderStorage orderStorage;
        #endregion

        #region ctor
        public OrderService()
        {
            orderStorage = DependencyService.Get<IOrderStorage>();
        }
        #endregion

        public async Task CreateAsync(User user, string train, int carriage, int place, IReadOnlyDictionary<Item, int> cart)
        {
            var order = new Order
            {
                Id = ObjectId.GenerateNewId(),
                CustomerId = user.Id,
                CustomerName = $"{user.LastName} {user.FirstName} {user.MiddleName}",
                CustomerPhone = user.Phone,
                Train = train,
                Carriage = carriage,
                Place = place,
                OrderItems = cart.Select(c => new OrderItem
                {
                    Id = ObjectId.GenerateNewId(),
                    ItemId = c.Key.Id,
                    ItemName = c.Key.Name,
                    Qty = c.Value
                }).ToList()
            };

            await orderStorage.CreateAsync(order);
        }
    }
}
