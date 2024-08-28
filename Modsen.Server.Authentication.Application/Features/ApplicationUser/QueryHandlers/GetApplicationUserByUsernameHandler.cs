using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Domain.Constants;
using Modsen.Server.Authentication.Domain.Exeptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUserByUsernameHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        : IRequestHandler<GetApplicationUserByUsername, Domain.Entities.ApplicationUser>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task<Domain.Entities.ApplicationUser> Handle(GetApplicationUserByUsername request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByNameAsync(request.Username)
                ?? throw new NotFoundException(ErrorConstants.NotFoundUserError);
        }
    }
}
