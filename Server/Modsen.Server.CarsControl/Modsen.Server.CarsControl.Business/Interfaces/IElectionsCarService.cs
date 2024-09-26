using Modsen.Server.CarsControl.Business.Interfaces.Base;
using Modsen.Server.CarsControl.Business.Models.Requests;
using Modsen.Server.Shared.Enums;

namespace Modsen.Server.CarsControl.Business.Interfaces
{
    public interface IElectionsCarService : ICarServiceBase
    {
        Task MoveAsync(MoveCar moveCar, CancellationToken cancellationToken = default);

        Task AddCarAsync(AddCar addCar);
    }
}
