using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingCarts.Model
{
    public class Item
    {
        public ObjectId Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public double Price { get; set; }
        public int Balance { get; set; } = 0;

        [BsonIgnore]
        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name) || Name.Length == 0)
                    return "?";

                return Name[0].ToString().ToUpper();
            }
        }
    }
}