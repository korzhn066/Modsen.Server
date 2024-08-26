using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.DataAccess.Repository
{
    internal class MongoRepositoryFactory<T> : IMongoRepositoryFactory<T> where T : class
    {
        private readonly IMongoDatabase _database;

        public MongoRepositoryFactory(IMongoDatabase database) 
        { 
            _database = database;
        }

        public IMongoRepository<T> Create(string collection)
        {
            return new MongoRepository<T>(_database, collection);
        }
    }
}
