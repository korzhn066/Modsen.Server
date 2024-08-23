using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Domain.Enums;

namespace Modsen.Server.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Route("deny_admin_role")]
        public async Task<IActionResult> DenyAdminRole(string userId)
        {
            await _mediator.Send(new DenyApplicationUserRoleById
            {
                RoleName = "Admin",
                UserId = userId
            });

            return NoContent();
        }

        [HttpPost]
        [Route("add_admin_role")]
        public async Task<IActionResult> AddAdminRole(string userId)
        {
            await _mediator.Send(new AddApplicationUserToRoleById
            {
                RoleName = "Admin",
                UserId = userId
            });

            return NoContent();
        }

        [HttpPost]
        [Route("change_user_status")]
        public async Task<IActionResult> ChangeUserStatus(string userId, UserStatus userStatus)
        {
            await _mediator.Send(new ChangeApplicationUserStatus
            {
                UserId = userId,
                Status = userStatus
            });

            return NoContent();
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers(int page, int count)
        {
            var users = await _mediator.Send(new GetApplicationUsers
            {
                Page = page,
                Count = count
            });

            return Ok(users);
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await _mediator.Send(new GetApplicationUserByUsername
            {
                Username = userName
            });

            return Ok(user);
        }
    }
}
