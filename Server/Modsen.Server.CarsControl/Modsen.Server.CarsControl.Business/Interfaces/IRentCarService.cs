using Modsen.Server.CarsControl.Business.Interfaces.Base;
using Modsen.Server.CarsControl.Business.Models.Requests;
using Modsen.Server.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.Business.Interfaces
{
    public interface IRentCarService : ICarServiceBase
    {
        Task MoveAsync(MoveCar moveCar, CancellationToken cancellationToken = default);

        Task AddCarAsync(AddCar addCar);
    }
}
