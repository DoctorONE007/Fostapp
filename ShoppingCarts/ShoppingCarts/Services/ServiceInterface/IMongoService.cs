using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCarts.Services.ServiceInterface
{
    public interface IMongoService
    {
        IMongoDatabase GetMongo();
    }
}
