using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElection.Domain.Entities;
using Modsen.Server.CarsElection.Domain.Enums;

namespace Modsen.Server.CarsElection.Infrastructure.Data.Seeds
{
    internal static class CommentSeeder
    {
        internal static void SeedComments(this ModelBuilder builder) 
        {
            var comments = new List<Comment> 
            { 
                new Comment
                {
                    Id = 1,
                    Message = "Some message",
                    Type = CommentType.Positive,
                    DateTime = DateTime.Now,
                    LikeCount = 1,
                    DislikeCount = 2,
                    CarId = "1"
                }
            };

            builder.Entity<Comment>().HasData(comments);
        }
    }
}
