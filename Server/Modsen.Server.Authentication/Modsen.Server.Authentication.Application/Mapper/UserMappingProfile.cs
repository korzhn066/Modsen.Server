using AutoMapper;
using Modsen.Server.Authentication.Application.Models.Responses;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Shared.Models.Kafka;

namespace Modsen.Server.Authentication.Application.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser, UserCreated>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.UserName));

            CreateMap<ApplicationUser, UserResponse>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.UserName))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.PhoneNumber))
                .ForMember(destination => destination.UserStatus, options => options.MapFrom(source => source.UserStatus));
        }
    }
}
