using AutoMapper;
using Modsen.Server.CarsElections.Api.Models.Requests;
using Modsen.Server.CarsElections.Application.Features.Like.Command;

namespace Modsen.Server.CarsElections.Api.Mapper
{
    public class LikeMappingProfile : Profile
    {
        public LikeMappingProfile()
        {
            CreateMap<LikeRequest, PutLike>()
                .ForMember(destination => destination.LikeType, options => options.MapFrom(source => source.LikeType))
                .ForMember(destination => destination.CommentId, options => options.MapFrom(source => source.CommentId));
        }
    }
}
