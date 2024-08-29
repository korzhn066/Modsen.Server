using AutoMapper;
using Modsen.Server.CarsElections.Api.Models.Requests;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Application.Features.Like.Command;

namespace Modsen.Server.CarsElections.Api.Mapper
{
    public class RequestMappingProfile : Profile
    {
        public RequestMappingProfile()
        {
            CreateMap<CommentRequest, AddComment>();
            CreateMap<LikeRequest, PutLike>();
        }
    }
}
