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

        public List<Order> GetAll() => 
            _orders.Find(order => true).ToList();

        public Order GetById(string id) => 
            _orders.Find(order => order.Id == id).FirstOrDefault();

        public Order Create(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void Update(string id, Order orderIn) =>
            _orders.ReplaceOne(order => order.Id == id, orderIn);
        

        public void Remove(Order orderIn) =>
            _orders.DeleteOne(order => order.Id == orderIn.Id);
        
    
        public void RemoveById(string id) =>
            _orders.DeleteOne(order => order.Id == id);
        
    }
}