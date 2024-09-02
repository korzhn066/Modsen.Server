using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElection.Domain.Entities;
using Modsen.Server.CarsElection.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElection.Infrastructure.Data.Seeds
{
    internal static class LikeSeeder
    {
        internal static void SeedLikes(this ModelBuilder builder)
        {
            var likes = new List<Like> 
            { 
                new Like
                {
                    Id = 1,
                    Type = LikeType.Like,
                    CommentId = 1,
                    UserId = "1"
                },
                new Like
                {
                    Id = 2,
                    Type = LikeType.Dislike,
                    CommentId = 1,
                    UserId = "2"
                },
                new Like
                {
                    Id = 3,
                    Type = LikeType.Dislike,
                    CommentId = 1,
                    UserId = "3"
                }
            };

            builder.Entity<Like>().HasData(likes);
        }
    }
}
