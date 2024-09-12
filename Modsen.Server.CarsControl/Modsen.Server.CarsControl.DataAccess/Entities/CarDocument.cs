using MongoDB.Bson;

namespace Modsen.Server.CarsControl.DataAccess.Entities
{
    public class CarDocument
    {
        public ObjectId Id { get; set; }
        public BsonArray Photos { get; set; } = new();
        public BsonDocument Content { get; set; } = null!;
    }
}
