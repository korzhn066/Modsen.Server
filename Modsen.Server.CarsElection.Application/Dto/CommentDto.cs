using Modsen.Server.CarsElection.Domain.Enums;

namespace Modsen.Server.CarsElection.Application.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public CommentType Type { get; set; } = CommentType.Positive;
        public DateTime DateTime { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public bool IsMyComment { get; set; }
    }
}
