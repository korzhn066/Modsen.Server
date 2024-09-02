using Modsen.Server.CarsElections.Api.Mapper;
using System.Text.Json.Serialization;

namespace Modsen.Server.CarsElections.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(CommentMappingProfile), typeof(LikeMappingProfile));

            return services;
        }
    }
}
