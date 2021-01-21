using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCarts.Model
{
    public class OrderItem
    {
        public ObjectId Id { get; set; }
        public ObjectId ItemId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
    }
}
