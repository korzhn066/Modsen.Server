using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Modsen.Server.CarsControl.DataAccess.Entities
{
    public class CarDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public BsonString Name { get; set; } = BsonString.Empty;

        [BsonElement("description")]
        public BsonString Description { get; set; } = BsonString.Empty;

        [BsonElement("photos")]
        public BsonArray Photos { get; set; } = [];

        [BsonElement("content")]
        public BsonDocument Content { get; set; } = null!;
    }
}
