using Modsen.Server.CarsControl.DataAccess.Entities;
using Modsen.Server.CarsControl.DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory
{
    public interface IMongoRepository
    {
        Task<List<CarDocument>> GetAllAsync(int page, int count, CancellationToken cancellationToken = default);

        Task<CarDocument?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        
        Task<string> AddAsync(CarDocument entity);
        
        Task<UpdateResult> UpdateAsync(UpdateCar updateCar);
        
        Task<DeleteResult> DeleteAsync(string id);
    }
}
