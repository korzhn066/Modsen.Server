using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory
{
    public interface IMongoRepositoryFactory<T> where T : class
    {
        IMongoRepository<T> Create(string collection);
    }
}
