using Modsen.Server.CarsControl.DataAccess.Models;

namespace Modsen.Server.CarsControl.Business.Interfaces.Base
{
    public interface ICarServiceBase
    {
        Task<string> GetCarAsync(string carId, CancellationToken cancellationToken);

        Task<string> GetCarsAsync(int page, int count, CancellationToken cancellationToken);
        
        Task UpdateCarAsync(UpdateCar updateCar);
        
        Task DeleteCarAsync(string carId);
    }
}
