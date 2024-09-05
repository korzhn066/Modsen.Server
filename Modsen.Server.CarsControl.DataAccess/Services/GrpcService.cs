using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;

namespace Modsen.Server.CarsControl.DataAccess.Services
{
    public class GrpcService(Car.CarClient carClient) : IGrpcService
    {
        private readonly Car.CarClient _carClient = carClient;

        public async Task AddCarAsync(string carId, CancellationToken cancellationToken = default)
        {
            await _carClient.AddCarAsync(new CarRequest { Id = carId }, cancellationToken: cancellationToken);
        }
    }
}
