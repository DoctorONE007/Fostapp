using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShoppingCarts.Helpers;
using ShoppingCarts.Model;
using ShoppingCarts.Services.ServiceInterface;
using Xamarin.Forms;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

[assembly: Dependency(typeof(ShoppingCarts.Storage.ItemStorage))]
namespace ShoppingCarts.Storage
{
    public class ItemStorage : IItemStorage
    {
        #region Fields
        private static IMongoService mongoService;
        private static string collectionName = "Items";
        #endregion

        #region ctor
        public ItemStorage()
        {
            mongoService = DependencyService.Get<IMongoService>();
        }
        #endregion

        #region IItemStorage
        public async Task<TryResult<IEnumerable<Item>>> GetItemsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var collection = GetCollection();

                if (collection == null)
                    throw new Exception("Нет подключения к БД");

                if (await collection.CountAsync(new BsonDocument()) == 0)
                    await InitDB();

                var items = await collection.AsQueryable()
                    .Where(u => u.Status)
                    .ToListAsync(cancellationToken);

                return new TryResult<IEnumerable<Item>>(items);
            }
            catch (Exception e)
            {
                return new TryResult<IEnumerable<Item>>(e);
            }
        }

        public async Task<TryResult<bool>> ChangeItemAsync(ObjectId id, int balance, CancellationToken cancellationToken = default)
        {
            try
            {
                var collection = GetCollection();

                if (collection == null)
                    throw new Exception("Нет подключения к БД");

                var filterDefinition = Builders<Item>.Filter.Eq("Id", id);
                var storedItem = await collection.Find(filterDefinition).Limit(1).SingleAsync();
                if (storedItem == null)
                {
                    return new TryResult<bool>(new KeyNotFoundException($"Id {id} not found!"));
                }
                storedItem.Balance -= balance;
                await collection.FindOneAndReplaceAsync(filterDefinition, storedItem);
                return new TryResult<bool>(true);
            }
            catch (Exception e)
            {
                return new TryResult<bool>(e);
            }
        }
        #endregion

        #region Functions
        private async Task InitDB()
        {
            var items = new List<Item>();

            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/11/25/856532_food_512x512.png",
                Name = "Бургер",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 200
            });

            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/07/26/801954_food_512x512.png",
                Name = "Пицца",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 200
            });

            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/08/18/812233_animal_512x512.png",
                Name = "Рыба",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 150
            });

            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/08/18/812731_food_512x512.png",
                Name = "Картофель фри",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 90
            });
            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/02/17/720501_food_512x512.png",
                Name = "Газированый напиток",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 80
            });
            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/02/17/720508_food_512x512.png",
                Name = "Блинчики",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 60
            });
            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/02/19/721754_food_512x512.png",
                Name = "Салат",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 120
            });
            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/01/01/696294_food_512x512.png",
                Name = "Фрукты",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 150
            });
            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/08/30/821164_food_512x512.png",
                Name = "Чипсы",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 100
            });
            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/08/19/816935_food_512x512.png",
                Name = "Суп",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 90
            });
            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/03/04/728852_lunch_512x512.png",
                Name = "Завтрак",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 180
            });
            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/03/04/728852_lunch_512x512.png",
                Name = "Обед",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 200
            });
            items.Add(new Item
            {
                Image = "https://www.shareicon.net/data/128x128/2016/03/04/728852_lunch_512x512.png",
                Name = "Ужин",
                Id = ObjectId.GenerateNewId(),
                Status = true,
                Description = Constants.Description,
                ShortDescription = Constants.ShortDescription,
                Price = 250
            });

            var collection = GetCollection();
            await collection.InsertManyAsync(items);
        }

        private IMongoCollection<Item> GetCollection()
        {
            return mongoService.GetMongo()?.GetCollection<Item>(collectionName);
        }
        #endregion
    }
}
