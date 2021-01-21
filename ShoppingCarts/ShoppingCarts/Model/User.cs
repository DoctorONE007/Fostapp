using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCarts.Model
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
    }
}
