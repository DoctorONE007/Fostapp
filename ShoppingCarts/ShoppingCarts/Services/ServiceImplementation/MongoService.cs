using MongoDB.Driver;
using ShoppingCarts.Services.ServiceInterface;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShoppingCarts.Services.ServiceImplementation.MongoService))]
namespace ShoppingCarts.Services.ServiceImplementation
{
    public class MongoService : IMongoService
    {
        private static string connectionString = "";
        public IMongoDatabase GetMongo()
        {
            var client = new MongoClient(connectionString);
            return client.GetDatabase("test");
        }                
    }
}
