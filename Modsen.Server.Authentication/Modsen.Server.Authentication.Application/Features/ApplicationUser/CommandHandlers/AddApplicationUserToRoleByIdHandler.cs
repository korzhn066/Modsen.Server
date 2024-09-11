using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class AddApplicationUserToRoleByIdHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ILogger<AddApplicationUserToRoleByIdHandler> logger)
        : IRequestHandler<AddApplicationUserToRoleById>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ILogger<AddApplicationUserToRoleByIdHandler> _logger = logger;

        public async Task Handle(AddApplicationUserToRoleById request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                _logger.LogError("User is null when trying to get a role");

                throw new NotFoundException(ErrorConstants.NotFoundUserError);
            }

            await _userManager.AddToRoleAsync(user, request.RoleName);

            _logger.LogInformation("User get role");
        }
    }
}
