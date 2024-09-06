using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class AddApplicationUserToRoleByIdHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        : IRequestHandler<AddApplicationUserToRoleById>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task Handle(AddApplicationUserToRoleById request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId) 
                ?? throw new NotFoundException(ErrorConstants.NotFoundUserError);

            await _userManager.AddToRoleAsync(user, request.RoleName);
        }
    }
}
