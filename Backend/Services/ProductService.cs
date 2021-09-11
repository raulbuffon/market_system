using Market_system.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Market_system.Services
{
    public class ProductService{
        private readonly IMongoCollection<Product> _products;

        public ProductService(IMarketDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>(settings.ProductsCollectionName);
        }

        public List<Product> GetAll() =>
            _products.Find(product => true).ToList();

        public Product GetById(string id) =>
            _products.Find<Product>(product => product.Id == id).FirstOrDefault();

        public Product GetByName(string productName) =>
            _products.Find<Product>(product => product.ProductName == productName).FirstOrDefault(); 
        
        public Product Create(Product product)
        {
            if(IsNameUnique(product.ProductName)){
                _products.InsertOne(product);
                return product;
            }
            
            return null;
        }

        public void Update(string id, Product productIn) =>
            _products.ReplaceOne(product => product.Id == id, productIn);

        public void Remove(Product productIn) =>
            _products.DeleteOne(product => product.Id == productIn.Id);

        public void RemoveById(string id) =>
            _products.DeleteOne(product => product.Id == id);

        private bool IsNameUnique(string productName) {
            return !(_products.Find<Product>(product => product.ProductName == productName).Any());
        }
    }
}
