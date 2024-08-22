using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Domain.Exeptions;

namespace Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands
{
    public class DenyApplicationUserRoleByIdUseCase(UserManager<Domain.Entities.ApplicationUser> userManager)
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task DenyRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException();

            await _userManager.RemoveFromRoleAsync(user, roleName);
        }
    }
}
