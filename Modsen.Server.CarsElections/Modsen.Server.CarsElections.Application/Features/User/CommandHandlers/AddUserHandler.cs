using AutoMapper;
using MediatR;
using Modsen.Server.CarsElections.Application.Features.User.Commands;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.User.CommandHandlers
{
    public class AddUserHandler(
        IUserRepository userRepository,
        IMapper mapper) : IRequestHandler<AddUser>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(AddUser request, CancellationToken cancellationToken)
        {
            await _userRepository.AddAsync(_mapper.Map<Domain.Entities.User>(request), cancellationToken);
            
            await _userRepository.SaveChangesAsync();
        }
    }
}
