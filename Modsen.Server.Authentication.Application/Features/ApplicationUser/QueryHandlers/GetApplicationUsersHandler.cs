using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Queries;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUsersHandler(GetApplicationUsersUseCase getApplicationUsersUseCase)
        : IRequestHandler<GetApplicationUsers, List<Domain.Entities.ApplicationUser>>
    {
        private readonly GetApplicationUsersUseCase _getApplicationUsersUseCase = getApplicationUsersUseCase;

        public async Task<List<Domain.Entities.ApplicationUser>> Handle(GetApplicationUsers request, CancellationToken cancellationToken)
        {
            return await _getApplicationUsersUseCase.GetUsersAsync(
                request.Page,
                request.Count,
                cancellationToken);
        }
    }
}
