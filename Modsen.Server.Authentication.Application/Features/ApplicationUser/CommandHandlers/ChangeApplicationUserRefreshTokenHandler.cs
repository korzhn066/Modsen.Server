using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Domain.Constants;
using Modsen.Server.Authentication.Domain.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class ChangeApplicationUserRefreshTokenHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        : IRequestHandler<ChangeApplicationUserRefreshToken>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task Handle(ChangeApplicationUserRefreshToken request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName) 
                ?? throw new NotFoundException(ErrorConstants.NotFoundUserError);

            user.RefreshToken = request.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(request.RefreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
        }
    }
}
