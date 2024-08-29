using Modsen.Server.CarsElections.Domain.Enums;;

namespace Modsen.Server.CarsElections.Domain.Entities
{
    public class Like
    {
        public Comment Comment { get; set; } = null!;
        public int CommentId { get; set; }

        public User User { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public LikeType Type { get; set; }
    }
}
