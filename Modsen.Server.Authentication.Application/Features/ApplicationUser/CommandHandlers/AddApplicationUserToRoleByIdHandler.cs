using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class AddApplicationUserToRoleByIdHandler : IRequestHandler<AddApplicationUserToRoleById>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;

        public AddApplicationUserToRoleByIdHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(AddApplicationUserToRoleById request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                throw new KeyNotFoundException();

            await _userManager.AddToRoleAsync(user, request.RoleName);
        }
    }
}
