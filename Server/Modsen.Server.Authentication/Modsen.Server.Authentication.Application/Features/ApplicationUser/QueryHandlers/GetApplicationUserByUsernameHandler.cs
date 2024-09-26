using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.Models.Responses;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUserByUsernameHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ILogger<GetApplicationUserByUsernameHandler> logger,
        IMapper mapper)
        : IRequestHandler<GetApplicationUserByUsername, UserResponse>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ILogger<GetApplicationUserByUsernameHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<UserResponse> Handle(GetApplicationUserByUsername request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is null)
            {
                _logger.LogError("User is null");

                throw new NotFoundException(ErrorConstants.NotFoundUserError);
            }

            var response = _mapper.Map<UserResponse>(user);
            
            var roles = await _userManager.GetRolesAsync(user);

            response.Role = string.Join(", ", roles);

            _logger.LogInformation("Get user");

            return response;
        }
    }
}
