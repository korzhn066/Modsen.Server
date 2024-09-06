using MassTransit;
using MediatR;
using Modsen.Server.CarsElections.Application.Features.User.Commands;
using Modsen.Server.Shared.Models.Kafka;

namespace Modsen.Server.CarsElections.Application.Services
{
    public class UserCreatedConsumer(IMediator mediator) : IConsumer<UserCreated>
    {
        private readonly IMediator _mediator = mediator;

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var addUser = new AddUser
            {
                Id = context.Message.Id,
                UserName = context.Message.UserName,
            };

            await _mediator.Send(addUser);

            await Task.CompletedTask;
        }
    }
}
