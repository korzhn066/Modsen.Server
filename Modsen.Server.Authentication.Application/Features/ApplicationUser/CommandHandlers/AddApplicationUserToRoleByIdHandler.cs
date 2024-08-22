using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands;
using Modsen.Server.Authentication.Domain.Exeptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class AddApplicationUserToRoleByIdHandler(AddApplicationUserToRoleByIdUseCase addApplicationUserToRoleByIdUseCase)
        : IRequestHandler<AddApplicationUserToRoleById>
    {
        private readonly AddApplicationUserToRoleByIdUseCase _addApplicationUserToRoleByIdUseCase = addApplicationUserToRoleByIdUseCase;

        public async Task Handle(AddApplicationUserToRoleById request, CancellationToken cancellationToken)
        {
            await _addApplicationUserToRoleByIdUseCase.AddToRoleAsync(
                request.UserId,
                request.RoleName);
        }
    }
}
