using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Enums;

namespace Modsen.Server.CarsElections.Infrastructure.Data.Seeds
{
    internal static class LikeSeeder
    {
        internal static void SeedLikes(this ModelBuilder builder)
        {
            var likes = new List<Like>
            {
                new() {
                    Type = LikeType.Owner,
                    CommentId = 1,
                    UserId = "1"
                },
                new() {
                    Type = LikeType.Dislike,
                    CommentId = 1,
                    UserId = "2"
                },
                new() {
                    Type = LikeType.Dislike,
                    CommentId = 1,
                    UserId = "3"
                }
            };

            builder.Entity<Like>().HasData(likes);
        }
    }
}
