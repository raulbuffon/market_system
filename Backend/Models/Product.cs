using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace market_system.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string ProductName { get; set;}

        public decimal Price { get; set;}
    }
}