using MediatR;

namespace Modsen.Server.CarsElections.Application.Features.User.Commands
{
    public class AddUser : IRequest
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set;} = null!;
    }
}
