using AutoMapper;
using Modsen.Server.CarsElections.Application.Features.User.Commands;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AddUser, User>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.UserName));
        }
    }
}
