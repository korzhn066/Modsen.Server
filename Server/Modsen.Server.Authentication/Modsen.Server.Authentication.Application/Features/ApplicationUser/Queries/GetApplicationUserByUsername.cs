using MediatR;
using Modsen.Server.Authentication.Application.Models.Responses;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries
{
    public record GetApplicationUserByUsername : IRequest<UserResponse>
    {
        public string Username { get; set; } = null!;
    }
}
