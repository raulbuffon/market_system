using Market_system.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Market_system.Services
{
    public class OrderService{
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IMarketDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.OrdersCollectionName);
        }
    }
}