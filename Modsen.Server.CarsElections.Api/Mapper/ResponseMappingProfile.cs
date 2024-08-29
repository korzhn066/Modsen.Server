using AutoMapper;
using Modsen.Server.CarsElections.Api.Models.Responses;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Api.Mapper
{
    public class ResponseMappingProfile : Profile
    {
        public ResponseMappingProfile()
        { 
            CreateMap<Comment, CommentResponse>();
        }
    }
}
