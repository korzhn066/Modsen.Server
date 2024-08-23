using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Domain.Exeptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class ChangeApplicationUserStatusHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        : IRequestHandler<ChangeApplicationUserStatus>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task Handle(ChangeApplicationUserStatus request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId) ?? throw new NotFoundException();

            user.UserStatus = request.Status;

            await _userManager.UpdateAsync(user);
        }
    }
}
