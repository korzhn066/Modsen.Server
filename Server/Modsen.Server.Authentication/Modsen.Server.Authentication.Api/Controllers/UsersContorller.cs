using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Domain.Enums;

namespace Modsen.Server.Authentication.Api.Controllers
{
    [Route("api/users/")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpDelete]
        [Route("admin-role")]
        public async Task<IActionResult> DenyAdminRole(string userId)
        {
            await _mediator.Send(new DenyApplicationUserRoleById
            {
                RoleName = "Admin",
                UserId = userId
            });

            return NoContent();
        }

        [HttpPut]
        [Route("admin-role")]
        public async Task<IActionResult> AddAdminRole(string userId)
        {
            await _mediator.Send(new AddApplicationUserToRoleById
            {
                RoleName = "Admin",
                UserId = userId
            });

            return NoContent();
        }

        [HttpPut]
        [Route("status")]
        public async Task<IActionResult> ChangeUserStatus(ChangeApplicationUserStatus changeApplicationUserStatus)
        {
            await _mediator.Send(changeApplicationUserStatus);

            return NoContent();
        }

        [HttpGet]
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
        [Route("{userName}")]
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
