using Modsen.Server.CarsElections.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Modsen.Server.CarsElections.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public CommentType Type { get; set; } = CommentType.Positive;
        public DateTime DateTime { get; set; }

        [Range(0, int.MaxValue)]
        public int LikeCount { get; set; }    
        [Range(0, int.MaxValue)]
        public int DislikeCount { get; set; }

        public virtual List<Like> Likes { get; set; } = [];

        public string CarId { get; set; } = null!;
        public Car Car { get; set; } = null!;
    }
}
