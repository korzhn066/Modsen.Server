using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class DenyApplicationUserRoleByIdHandler : IRequestHandler<DenyApplicationUserRoleById>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;

        public DenyApplicationUserRoleByIdHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(DenyApplicationUserRoleById request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            await _userManager.RemoveFromRoleAsync(user, request.RoleName);
        }
    }
}
