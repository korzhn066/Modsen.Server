namespace Modsen.Server.CarsControl.DataAccess.Interfaces.Services
{
    public interface IGrpcService
    {
        Task AddCarAsync(string carId, CancellationToken cancellationToken = default);
    }
}
