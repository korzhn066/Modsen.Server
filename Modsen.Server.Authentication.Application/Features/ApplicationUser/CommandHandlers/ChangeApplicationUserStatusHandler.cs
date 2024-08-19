using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class ChangeApplicationUserStatusHandler : IRequestHandler<ChangeApplicationUserStatus>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;

        public ChangeApplicationUserStatusHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(ChangeApplicationUserStatus request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                throw new KeyNotFoundException(nameof(user));

            user.UserStatus = request.Status;

            await _userManager.UpdateAsync(user);
        }
    }
}
