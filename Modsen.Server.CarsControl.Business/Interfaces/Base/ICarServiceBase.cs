using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.Interfaces.Base
{
    public interface ICarServiceBase
    {
        Task<string> GetCarAsync(string carId, CancellationToken cancellationToken);
        Task<string> GetCarsAsync(int page, int count, CancellationToken cancellationToken);
        Task UpdateCarAsync(string carId, string car);
        Task AddCarAsync(string car);
        Task DeleteCarAsync(string carId);
    }
}
