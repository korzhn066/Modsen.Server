using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands;
using Modsen.Server.Authentication.Domain.Exeptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class ChangeApplicationUserStatusHandler(ChangeApplicationUserStatusHandlerUseCase changeApplicationUserStatusHandlerUseCase)
        : IRequestHandler<ChangeApplicationUserStatus>
    {
        private readonly ChangeApplicationUserStatusHandlerUseCase _changeApplicationUserStatusHandlerUseCase 
            = changeApplicationUserStatusHandlerUseCase;

        public async Task Handle(ChangeApplicationUserStatus request, CancellationToken cancellationToken)
        {
            await _changeApplicationUserStatusHandlerUseCase.ChangeUserStatusAsync(
                request.UserId,
                request.Status);
        }
    }
}
