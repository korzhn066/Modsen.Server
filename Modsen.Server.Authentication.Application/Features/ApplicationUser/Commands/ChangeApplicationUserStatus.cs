using MediatR;
using Modsen.Server.Authentication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public record ChangeApplicationUserStatus : IRequest
    {
        public string UserId { get; set; } = null!;
        public UserStatus Status { get; set; }
    }
}
