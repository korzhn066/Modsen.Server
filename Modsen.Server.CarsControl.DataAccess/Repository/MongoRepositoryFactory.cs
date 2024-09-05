using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Driver;

namespace Modsen.Server.CarsControl.DataAccess.Repository
{
    internal class MongoRepositoryFactory : IMongoRepositoryFactory
    {
        private readonly IMongoDatabase _database;

        public MongoRepositoryFactory(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoRepository Create(string collection)
        {
            return new MongoRepository(_database, collection);
        }
    }
}
