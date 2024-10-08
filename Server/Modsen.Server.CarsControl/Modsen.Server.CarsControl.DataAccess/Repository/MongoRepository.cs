using Modsen.Server.CarsControl.DataAccess.Entities;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Modsen.Server.CarsControl.DataAccess.Repository
{
    internal class MongoRepository(IMongoDatabase database, string collectionName) : IMongoRepository
    {
        private readonly IMongoCollection<CarDocument> _collection = database.GetCollection<CarDocument>(collectionName);

        public async Task<List<CarDocument>> GetAllAsync(int page, int count, CancellationToken cancellationToken = default)
        {
            return await _collection
                .Find(new BsonDocument())
                .Skip((page - 1) * count)
                .Limit(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<CarDocument?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _collection
                .Find(Builders<CarDocument>.Filter.Eq("Id", new ObjectId(id)))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<string> AddAsync(CarDocument entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity.Id.ToString();
        }

        public async Task<UpdateResult> UpdateAsync(UpdateCar updateCar)
        {
            var filter = Builders<CarDocument>.Filter.Eq("Id", new ObjectId(updateCar.Id));
            var update = Builders<CarDocument>.Update
                .Set(carDocument => carDocument.Name, updateCar.Name)
                .Set(carDocument => carDocument.Description, updateCar.Description)
                .Set(carDocument => carDocument.Content, BsonDocument.Parse(updateCar.Json));

            return await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<DeleteResult> DeleteAsync(string id)
        {
            return await _collection.DeleteOneAsync(
                Builders<CarDocument>.Filter.Eq("Id", new ObjectId(id)));
        }
    }
}
