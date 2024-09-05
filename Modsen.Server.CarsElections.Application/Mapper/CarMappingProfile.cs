using AutoMapper;
using Modsen.Server.CarsElections.Application.Features.Car.Commands;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Mapper
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<AddCar, Car>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));
        }
    }
}
