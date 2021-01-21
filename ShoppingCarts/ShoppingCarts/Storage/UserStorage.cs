using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ShoppingCarts.Helpers;
using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShoppingCarts.Storage.UserStorage))]
namespace ShoppingCarts.Storage
{
    public class UserStorage : IUserStorage
    {
        #region Fields
        private static IMongoService mongoService;
        private static string collectionName = "Users";
        #endregion

        #region ctor
        public UserStorage()
        {
            mongoService = DependencyService.Get<IMongoService>();
        }
        #endregion

        #region IUserStorage
        public async Task<TryResult<User>> GetUserByPhoneAndPassAsync(string phone, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var users = GetCollection();
                if (users == null)
                    throw new Exception("Нет подключения к БД");

                var user = await users.AsQueryable()
                    .Where(u => u.Phone == phone)
                    .Where(u => u.PasswordHash == password)
                    .FirstOrDefaultAsync(cancellationToken);

                return new TryResult<User>(user);
            }
            catch (Exception e)
            {
                return new TryResult<User>(e);
            }
        }

        public async Task<TryResult<User>> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                var users = GetCollection();
                if (users == null)
                    throw new Exception("Нет подключения к БД");

                var existUser = await users.AsQueryable()
                    .Where(u => u.Phone == user.Phone)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existUser != null)
                {
                    throw new Exception("Пользователь с таким телефоном уже существует!");
                }

                if (user.Id == null)
                    user.Id = ObjectId.GenerateNewId();

                await users.InsertOneAsync(user, cancellationToken);

                return new TryResult<User>(user);
            }
            catch (Exception e)
            {
                return new TryResult<User>(e);
            }
        } 
        #endregion

        #region Functions
        private IMongoCollection<User> GetCollection()
        {
            return mongoService.GetMongo()?.GetCollection<User>(collectionName);
        } 
        #endregion
    }
}
