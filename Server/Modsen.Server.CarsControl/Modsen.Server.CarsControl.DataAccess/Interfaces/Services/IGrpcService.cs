namespace Modsen.Server.CarsControl.DataAccess.Interfaces.Services
{
    public interface IGrpcService
    {
        Task AddCarAsync(CarRequest carRequest, CancellationToken cancellationToken = default);

        Task DeleteCarAsync(string carId);
        
        Task UpdateCarAsync(UpdateCarRequest updateCarRequest, CancellationToken cancellationToken = default);
    }
}
