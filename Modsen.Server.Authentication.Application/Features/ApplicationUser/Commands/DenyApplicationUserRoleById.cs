using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public record DenyApplicationUserRoleById : IRequest
    {
        public string UserId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
}
