using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCarts.Model
{
    public class Order
    {
        public ObjectId Id { get; set; }
        public string CustomerName { get; set; }
        public ObjectId CustomerId { get; set; }
        public string CustomerPhone { get; set; }
        public string Train { get; set; }
        public int Carriage { get; set; }
        public int Place { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
