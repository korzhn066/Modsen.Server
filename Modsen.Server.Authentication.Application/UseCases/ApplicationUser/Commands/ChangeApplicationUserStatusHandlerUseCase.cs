using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Authentication.Domain.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands
{
    public class ChangeApplicationUserStatusHandlerUseCase(UserManager<Domain.Entities.ApplicationUser> userManager)
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task ChangeUserStatusAsync(string userId, UserStatus status)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException();

            user.UserStatus = status;

            await _userManager.UpdateAsync(user);
        }
    }
}
