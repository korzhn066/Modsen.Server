using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
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

        public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var item = await _collection
                .Find(Builders<T>.Filter.Eq("_id", new ObjectId(id)))
                .FirstOrDefaultAsync(cancellationToken);

            return (T?)item ?? throw new NotFoundException(ErrorConstants.NotFoundEntityError);
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, T entity)
        {
            var result = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)), entity);

            if (!result.IsAcknowledged)
            {
                throw new NotFoundException(ErrorConstants.NotFoundEntityError);
            }
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)));

            if (result.DeletedCount < 1)
            {
                throw new NotFoundException(ErrorConstants.NotFoundEntityError);
            }
        }
    }
}
