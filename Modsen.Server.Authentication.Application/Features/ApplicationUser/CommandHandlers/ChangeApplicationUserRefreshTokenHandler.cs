using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands;
using Modsen.Server.Authentication.Domain.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class ChangeApplicationUserRefreshTokenHandler(ChangeApplicationUserRefreshTokenUseCase changeApplicationUserRefreshTokenUseCase) 
        : IRequestHandler<ChangeApplicationUserRefreshToken>
    {
        private readonly ChangeApplicationUserRefreshTokenUseCase _changeApplicationUserRefreshTokenUseCase = changeApplicationUserRefreshTokenUseCase;

        public async Task Handle(ChangeApplicationUserRefreshToken request, CancellationToken cancellationToken)
        {
            await _changeApplicationUserRefreshTokenUseCase.ChangeRefreshTokenAsync(
                request.UserName,
                request.RefreshToken,
                request.RefreshTokenValidityInDays);
        }
    }
}
