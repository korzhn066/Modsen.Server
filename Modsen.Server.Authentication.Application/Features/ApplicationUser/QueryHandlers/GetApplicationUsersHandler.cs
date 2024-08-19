using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUsersHandler : IRequestHandler<GetApplicationUsers, List<Domain.Entities.ApplicationUser>>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;
        
        public GetApplicationUsersHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<Domain.Entities.ApplicationUser>> Handle(GetApplicationUsers request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                .Skip(request.Page * request.Count)
                .Take(request.Count)
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}
