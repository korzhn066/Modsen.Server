using Modsen.Server.CarsElection.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElection.Domain.Entities
{
    public class Like
    {
        public int Id { get; set; } 
        public LikeType Type { get; set; }

        public Comment Comment { get; set; } = null!;
        public int CommentId { get; set; }

        public User User { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
