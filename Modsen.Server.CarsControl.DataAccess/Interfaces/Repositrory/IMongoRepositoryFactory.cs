namespace Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory
{
    public interface IMongoRepositoryFactory
    {
        IMongoRepository Create(string collection);
    }
}
