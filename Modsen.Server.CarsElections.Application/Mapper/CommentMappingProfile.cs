using AutoMapper;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Mapper
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<AddComment, Comment>()
                .ForMember(destination => destination.CarId, options => options.MapFrom(source => source.CarId))
                .ForMember(destination => destination.Message, options => options.MapFrom(source => source.Message))
                .ForMember(destination => destination.Type, options => options.MapFrom(source => source.CommentType))
                .ForMember(destination => destination.DateTime, options => options.MapFrom(source => source.CreatedAt));
        }
    }
}
