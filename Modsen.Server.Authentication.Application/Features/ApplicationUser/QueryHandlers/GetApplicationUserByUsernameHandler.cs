using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUserByUsernameHandler : IRequestHandler<GetApplicationUserByUsername, Domain.Entities.ApplicationUser>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;

        public GetApplicationUserByUsernameHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Domain.Entities.ApplicationUser> Handle(GetApplicationUserByUsername request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is null)
                throw new KeyNotFoundException(nameof(user));

            return user;
        }
    }
}
