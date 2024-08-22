using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Domain.Exeptions;

namespace Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Queries
{
    public class GetApplicationUserByUsernameUseCases(UserManager<Domain.Entities.ApplicationUser> userManager)
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task<Domain.Entities.ApplicationUser> GetAsync(string username)
        {
            return await _userManager.FindByNameAsync(username) ?? throw new NotFoundException();
        }
    }
}
