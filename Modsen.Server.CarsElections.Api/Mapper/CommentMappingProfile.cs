using AutoMapper;
using Modsen.Server.CarsElections.Api.Models.Requests;
using Modsen.Server.CarsElections.Api.Models.Responses;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Api.Mapper
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<CommentRequest, AddComment>()
                .ForMember(destination => destination.CarId, options => options.MapFrom(source => source.CarId))
                .ForMember(destination => destination.Message, options => options.MapFrom(source => source.Message))
                .ForMember(destination => destination.CreatedAt, options => options.MapFrom(source => source.CreatedAt))
                .ForMember(destination => destination.CommentType, options => options.MapFrom(source => source.CommentType));

            CreateMap<Comment, CommentResponse>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.Message, options => options.MapFrom(source => source.Message))
                .ForMember(destination => destination.Type, options => options.MapFrom(source => source.Type))
                .ForMember(destination => destination.DateTime, options => options.MapFrom(source => source.DateTime))
                .ForMember(destination => destination.LikeCount, options => options.MapFrom(source => source.LikeCount))
                .ForMember(destination => destination.DislikeCount, options => options.MapFrom(source => source.DislikeCount));
        }
    }
}
