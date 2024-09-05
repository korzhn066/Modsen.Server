using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using Modsen.Server.Shared.Models.Kafka;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class RegisterApplicationUserHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ITokenProviderService tokenProviderService,
        ITopicProducer<UserCreated> topicProducer,
        IMapper mapper)
        : IRequestHandler<RegisterApplicationUser, TokenModel>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ITokenProviderService _tokenProviderService = tokenProviderService;
        private readonly ITopicProducer<UserCreated> _topicProducer = topicProducer;
        private readonly IMapper _mapper = mapper;   

        public async Task<TokenModel> Handle(RegisterApplicationUser request, CancellationToken cancellationToken)
        {
            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            var user = new Domain.Entities.ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = request.RegisterModel.PhoneNumber,
                UserName = request.RegisterModel.UserName,
                UserStatus = UserStatus.Unban,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(request.RefreshTokenValidityInDays)
            };

            var result = await _userManager.CreateAsync(user, request.RegisterModel.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException(string.Join(' ', result.Errors.Select(error => error.Code)));
            }

            result = await _userManager.AddToRoleAsync(user, "Client");

            if (!result.Succeeded)
            {
                throw new DbUpdateException(ErrorConstants.ServerSideError);
            }

            await _topicProducer.Produce(
                _mapper.Map<UserCreated>(user),
                cancellationToken);

            return new TokenModel
            {
                AccessToken = _tokenProviderService.GenerateAccessToken(user, ["Client"]),
                RefreshToken = refreshToken
            };
        }
    }
}
