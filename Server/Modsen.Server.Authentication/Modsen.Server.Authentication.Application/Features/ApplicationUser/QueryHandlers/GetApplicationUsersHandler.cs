using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.Models.Responses;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUsersHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ILogger<GetApplicationUsersHandler> logger,
        IMapper mapper)
        : IRequestHandler<GetApplicationUsers, List<UserResponse>>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ILogger<GetApplicationUsersHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<List<UserResponse>> Handle(GetApplicationUsers request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get users");

            var users = await _userManager.Users
                .Skip((request.Page - 1) * request.Count)
                .Take(request.Count)
                .ToListAsync(cancellationToken);

            var response = new List<UserResponse>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userResponse = _mapper.Map<UserResponse>(user);
                userResponse.Role = string.Join(", ", roles);

                response.Add(userResponse);
            }

            return response;
        }
    }
}
