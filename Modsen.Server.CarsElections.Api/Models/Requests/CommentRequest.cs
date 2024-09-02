using Modsen.Server.CarsElections.Domain.Enums;

namespace Modsen.Server.CarsElections.Api.Models.Requests
{
    public class CommentRequest
    {
        public string CarId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public CommentType CommentType { get; set; }
    }
}
