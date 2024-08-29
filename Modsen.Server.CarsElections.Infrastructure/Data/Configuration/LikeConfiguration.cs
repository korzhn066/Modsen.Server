using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Infrastructure.Data.Configuration
{
    internal static class LikeConfiguration
    {
        public static void ConfigureLikes(this ModelBuilder builder)
        {
            builder.Entity<Like>()
                .HasKey(like => new { like.UserId, like.CommentId });
        }
    }
}
