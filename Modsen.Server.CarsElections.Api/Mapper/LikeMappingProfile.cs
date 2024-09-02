using AutoMapper;
using Modsen.Server.CarsElections.Api.Models.Requests;
using Modsen.Server.CarsElections.Application.Features.Like.Command;

namespace Modsen.Server.CarsElections.Api.Mapper
{
    public class LikeMappingProfile : Profile
    {
        public LikeMappingProfile()
        {
            CreateMap<LikeRequest, PutLike>();
        }
    }
}
