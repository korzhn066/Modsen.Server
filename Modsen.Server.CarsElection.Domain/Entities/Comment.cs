using Modsen.Server.CarsElection.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElection.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public CommentType Type { get; set; } = CommentType.Positive;
        public DateTime DateTime { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        public virtual List<Like> Likes { get; set; } = new();

        public string CarId { get; set; } = null!;
        public Car Car { get; set; } = null!;
    }
}
