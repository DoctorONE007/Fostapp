using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingCarts.Helpers;
using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShoppingCarts.Storage.OrderStorage))]
namespace ShoppingCarts.Storage
{
    public class OrderStorage : IOrderStorage
    {
        #region Fields
        private static IMongoService mongoService;
        private static string collectionName = "Orders";
        #endregion

        public OrderStorage()
        {
            mongoService = DependencyService.Get<IMongoService>();
        }

        #region IOrderStorage
        public async Task<TryResult<Order>> CreateAsync(Order order, CancellationToken cancellationToken = default)
        {
            try
            {
                var orders = GetCollection();
                if (orders == null)
                    throw new Exception("Нет подключения к БД");

                if (order.Id == null)
                    order.Id = ObjectId.GenerateNewId();

                await orders.InsertOneAsync(order, cancellationToken);

                return new TryResult<Order>(order);
            }
            catch (Exception e)
            {
                return new TryResult<Order>(e);
            }
        } 
        #endregion

        #region Fields
        private IMongoCollection<Order> GetCollection()
        {
            return mongoService.GetMongo()?.GetCollection<Order>(collectionName);
        }
        #endregion
    }
}
