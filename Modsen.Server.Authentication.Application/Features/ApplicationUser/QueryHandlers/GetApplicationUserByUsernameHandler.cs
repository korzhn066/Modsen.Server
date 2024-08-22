using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Queries;
using Modsen.Server.Authentication.Domain.Exeptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUserByUsernameHandler(GetApplicationUserByUsernameUseCases getApplicationUserByUsernameUseCases)
        : IRequestHandler<GetApplicationUserByUsername, Domain.Entities.ApplicationUser>
    {
        private readonly GetApplicationUserByUsernameUseCases _getApplicationUserByUsernameUseCases 
            = getApplicationUserByUsernameUseCases;

        public async Task<Domain.Entities.ApplicationUser> Handle(GetApplicationUserByUsername request, CancellationToken cancellationToken)
        {
            return await _getApplicationUserByUsernameUseCases.GetAsync(
                request.Username);
        }
    }
}
