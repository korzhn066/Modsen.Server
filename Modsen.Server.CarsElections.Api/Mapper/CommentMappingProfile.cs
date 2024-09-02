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
            CreateMap<CommentRequest, AddComment>();
            CreateMap<Comment, CommentResponse>();
        }
    }
}
