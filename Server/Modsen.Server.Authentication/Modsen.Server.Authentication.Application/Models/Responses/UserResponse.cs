using Modsen.Server.Authentication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Models.Responses
{
    public class UserResponse
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public UserStatus UserStatus { get; set; }
        public string Role { get; set; } = null!;
    }
}
