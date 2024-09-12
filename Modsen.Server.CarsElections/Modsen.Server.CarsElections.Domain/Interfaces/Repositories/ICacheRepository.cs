namespace Modsen.Server.CarsElections.Domain.Interfaces.Repositories
{
    public interface ICacheRepository
    {
        Task SetAsync<T>(string key, T value, int ttl);
        Task SetAsync<T>(string key, T value);
        Task<T?> GetAsync<T>(string key);
    }
}
