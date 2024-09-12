using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications;
using Modsen.Server.CarsElections.Application.Specifications.UserSpecifications;
using Modsen.Server.Shared.Constants;
using Modsen.Server.CarsElections.Domain.Enums;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Modsen.Server.CarsElections.Application.Features.Comment.CommandHandlers
{
    public class AddCommentHandler(
        ILikeRepository likeRepository,
        IUserRepository userRepository,
        ILogger<AddCommentHandler> logger
        ICacheRepository cacheRepository,
        IMapper mapper) : IRequestHandler<AddComment>
    {
        private readonly ILikeRepository _likeRepository = likeRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICacheRepository _cacheRepository = cacheRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AddCommentHandler> _logger = logger;

        public async Task Handle(AddComment request, CancellationToken cancellationToken)
        {
            await CheckIsCommentExist(request.CarId, request.UserName, cancellationToken);

            var comment = _mapper.Map<Domain.Entities.Comment>(request);

            var user = await _userRepository.Query
                .GetQuery(new GetUserByUserNameSpecification(request.UserName))
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                _logger.LogError("User is null when trying to add comment");

                throw new NotFoundException(ErrorConstants.NotFoundUserError);
            }
         
            await _likeRepository.AddAsync(new Domain.Entities.Like
            {
                Comment = comment,
                User = user,
                Type = LikeType.Owner,
            }, cancellationToken);

            await _cacheRepository.SetAsync(request.UserName + request.CarId, comment);

            await _likeRepository.SaveChangesAsync();

            _logger.LogInformation("Add comment");
        }

        private async Task CheckIsCommentExist(string carId, string userName, CancellationToken cancellationToken)
        {
            var like = await _likeRepository.Query
                .GetQuery(new LikeCarIdSpecification(carId))
                .GetQuery(new LikeUsernameSpecification(userName))
                .GetQuery(new LikeIsOwnerSpecification())
                .FirstOrDefaultAsync(cancellationToken);

            if (like is not null)
            {
                _logger.LogError("User already has a comment");

                throw new BadRequestException(ErrorConstants.CommentAlreadyExistsError);
            }
        }
    }
}
