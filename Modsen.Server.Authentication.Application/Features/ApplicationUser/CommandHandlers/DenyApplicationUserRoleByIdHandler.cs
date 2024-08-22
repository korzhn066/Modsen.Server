using MediatR;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class DenyApplicationUserRoleByIdHandler(DenyApplicationUserRoleByIdUseCase denyApplicationUserRoleByIdUseCase)
        : IRequestHandler<DenyApplicationUserRoleById>
    {
        private readonly DenyApplicationUserRoleByIdUseCase _denyApplicationUserRoleByIdUseCase 
            = denyApplicationUserRoleByIdUseCase;

        public async Task Handle(DenyApplicationUserRoleById request, CancellationToken cancellationToken)
        {
            await _denyApplicationUserRoleByIdUseCase.DenyRoleAsync(
                request.UserId,
                request.RoleName);
        }
    }
}
