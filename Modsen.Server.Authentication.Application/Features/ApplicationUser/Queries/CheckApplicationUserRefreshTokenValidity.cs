using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries
{
    public record CheckApplicationUserRefreshTokenValidity : IRequest<bool>
    {
        public string UserName { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
