using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;

namespace Modsen.Server.CarsControl.DataAccess.Services
{
    public class GrpcService(Car.CarClient carClient) : IGrpcService
    {
        private readonly Car.CarClient _carClient = carClient;

        public async Task AddCarAsync(CarRequest carRequest, CancellationToken cancellationToken = default)
        {
            await _carClient.AddCarAsync(carRequest, cancellationToken: cancellationToken);
        }

        public async Task DeleteCarAsync(string carId)
        {
            await _carClient.DeleteCarAsync(new DeleteCarRequest { Id = carId });
        }

        public async Task UpdateCarAsync(UpdateCarRequest updateCarRequest, CancellationToken cancellationToken = default)
        {
            await _carClient.UpdateCarAsync(updateCarRequest, cancellationToken: cancellationToken);
        }
    }
}
