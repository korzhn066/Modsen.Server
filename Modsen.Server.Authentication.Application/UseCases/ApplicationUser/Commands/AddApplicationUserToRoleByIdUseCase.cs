using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Domain.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands
{
    public class AddApplicationUserToRoleByIdUseCase(UserManager<Domain.Entities.ApplicationUser> userManager)
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task AddToRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException();

            await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
