using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Queries
{
    public class GetApplicationUsersUseCase(UserManager<Domain.Entities.ApplicationUser> userManager)
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task<List<Domain.Entities.ApplicationUser>> GetUsersAsync(int page, int count, CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .Skip(page * count)
                .Take(count)
                .ToListAsync(cancellationToken);
        }
    }
}
