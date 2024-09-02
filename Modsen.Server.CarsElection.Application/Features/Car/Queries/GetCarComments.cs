using MediatR;
using Modsen.Server.CarsElection.Domain.Entities;

namespace Modsen.Server.CarsElection.Application.Features.Car.Queries
{
    public class GetCarComments : IRequest<List<Comment>>
    {


        public GetCarComments() { }
    }
}
