
using Infrastructuur.Repositories.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructuur.Repositories.Classes
{
    public class MongoRepository<T> : IMongoRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(string connectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        // Create
        public async Task<bool> InsertAsync(T item)
        {
            var oldCount = (await GetAllAsync()).Count();

            await _collection.InsertOneAsync(item);

            var newCount = (await GetAllAsync()).Count();
            return oldCount < newCount;
        }
    }
}
