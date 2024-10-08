using MediatR;
using Modsen.Server.Authentication.Application.Models.Responses;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries
{
    public record GetApplicationUsers : IRequest<List<UserResponse>>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
