using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Modsen.Server.CarsControl.DataAccess.Repository
{
    internal class MongoRepository<T>(IMongoDatabase database, string collectionName) : IMongoRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection = database.GetCollection<T>(collectionName);

        public async Task<List<T>> GetAllAsync(int page, int count, CancellationToken cancellationToken = default)
        {
            return await _collection
                .Find(new BsonDocument())
                .Skip((page - 1) * count)
                .Limit(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _collection
                .Find(Builders<T>.Filter.Eq("_id", new ObjectId(id)))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<ReplaceOneResult> UpdateAsync(string id, T entity)
        {
            return await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)), entity);
        }

        public async Task<DeleteResult> DeleteAsync(string id)
        {
            return await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)));
        }
    }
}
