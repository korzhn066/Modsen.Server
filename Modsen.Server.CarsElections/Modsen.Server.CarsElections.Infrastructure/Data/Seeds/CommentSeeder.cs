using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Enums;

namespace Modsen.Server.CarsElections.Infrastructure.Data.Seeds
{
    internal static class CommentSeeder
    {
        internal static void SeedComments(this ModelBuilder builder)
        {
            var comments = new List<Comment>
            {
                new() {
                    Id = 1,
                    Message = "Some message",
                    Type = CommentType.Negative,
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
