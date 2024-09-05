using MongoDB.Bson;

namespace Modsen.Server.CarsControl.DataAccess.Entities
{
    public class CarDocument
    {
        public ObjectId Id { get; set; }
        public BsonDocument Content { get; set; } = null!;
    }
}
