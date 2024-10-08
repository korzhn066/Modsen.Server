using AutoMapper;
using Modsen.Server.CarsElections.Application.Features.Car.Commands;

namespace Modsen.Server.CarsElections.Api.Mapper
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile() 
        {
            CreateMap<CarRequest, AddCar>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.CarType, options => options.MapFrom(source => (Shared.Enums.CarType)source.CarType));

            CreateMap<DeleteCarRequest, DeleteCar>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));

            CreateMap<UpdateCarRequest, UpdateCar>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.CarType, options => options.MapFrom(source => (Shared.Enums.CarType)source.CarType));
        }
    }
}
