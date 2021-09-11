using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Market_system.Models
{
    public class Order
    {
        [BsonId]
        [JsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public decimal TotalPrice { get; set;}

        public List<Product> Products { get; set;} = new List<Product>();
    }
}