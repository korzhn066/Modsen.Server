using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class ChangeApplicationUserStatusHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ILogger<ChangeApplicationUserStatusHandler> logger)
        : IRequestHandler<ChangeApplicationUserStatus>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ILogger<ChangeApplicationUserStatusHandler> _logger = logger;

        public async Task Handle(ChangeApplicationUserStatus request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                _logger.LogError("User is null when trying to change status");

                throw new NotFoundException(ErrorConstants.NotFoundUserError);
            }

            user.UserStatus = request.Status;

            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User change status");
        }
    }
}
