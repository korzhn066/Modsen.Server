using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUserByUsernameHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ILogger<GetApplicationUserByUsernameHandler> logger)
        : IRequestHandler<GetApplicationUserByUsername, Domain.Entities.ApplicationUser>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ILogger<GetApplicationUserByUsernameHandler> _logger = logger;

        public async Task<Domain.Entities.ApplicationUser> Handle(GetApplicationUserByUsername request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is null)
            {
                _logger.LogError("User is null");

                throw new NotFoundException(ErrorConstants.NotFoundUserError);
            }

            _logger.LogInformation("Get user");

            return user;
        }
    }
}
