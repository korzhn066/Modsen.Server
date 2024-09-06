using Modsen.Server.CarsElections.Domain.Enums;

namespace Modsen.Server.CarsElections.Api.Models.Responses
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public CommentType Type { get; set; } = CommentType.Positive;
        public DateTime DateTime { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
    }
}
