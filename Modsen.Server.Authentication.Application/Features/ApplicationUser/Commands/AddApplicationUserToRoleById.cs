using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public record AddApplicationUserToRoleById : IRequest
    {
        public string RoleName { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
