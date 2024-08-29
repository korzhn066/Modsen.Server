using Modsen.Server.CarsElections.Domain.Enums;

namespace Modsen.Server.CarsElections.Api.Models.Requests
{
    public class LikeRequest
    {
        public LikeType LikeType { get; set; }
        public int CommentId { get; set; }
    }
}
