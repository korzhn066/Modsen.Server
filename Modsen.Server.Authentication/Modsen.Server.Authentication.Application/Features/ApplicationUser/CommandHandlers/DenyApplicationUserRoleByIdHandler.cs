using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class DenyApplicationUserRoleByIdHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        : IRequestHandler<DenyApplicationUserRoleById>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task Handle(DenyApplicationUserRoleById request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId) 
                ?? throw new NotFoundException(ErrorConstants.NotFoundUserError);

            await _userManager.RemoveFromRoleAsync(user, request.RoleName);
        }
    }
}
