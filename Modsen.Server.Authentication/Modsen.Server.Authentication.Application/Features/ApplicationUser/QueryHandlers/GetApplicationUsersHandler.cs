using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUsersHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ILogger<GetApplicationUsersHandler> logger)
        : IRequestHandler<GetApplicationUsers, List<Domain.Entities.ApplicationUser>>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ILogger<GetApplicationUsersHandler> _logger = logger;

        public async Task<List<Domain.Entities.ApplicationUser>> Handle(GetApplicationUsers request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get users");

            return await _userManager.Users
                .Skip((request.Page - 1) * request.Count)
                .Take(request.Count)
                .ToListAsync(cancellationToken);
        }
    }
}
