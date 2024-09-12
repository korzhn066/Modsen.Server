using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Modsen.Server.CarsElections.Application.Features.User.Commands;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.User.CommandHandlers
{
    public class AddUserHandler(
        IUserRepository userRepository,
        IMapper mapper,
        ILogger<AddUserHandler> logger) : IRequestHandler<AddUser>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AddUserHandler> _logger = logger;

        public async Task Handle(AddUser request, CancellationToken cancellationToken)
        {
            await _userRepository.AddAsync(_mapper.Map<Domain.Entities.User>(request), cancellationToken);

            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("Add user");
        }
    }
}
