using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Comment.Queries;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Modsen.Server.CarsElections.Application.Features.Comment.QueryHandlers
{
    public class GetCommentByUserNameHandler(
        ILikeRepository likeRepository,
        ILogger<GetCommentByUserNameHandler> logger)
        : IRequestHandler<GetCommentByUserName, Domain.Entities.Comment>
    {
        private readonly ILikeRepository _likeRepository = likeRepository;
        private readonly ILogger<GetCommentByUserNameHandler> _logger = logger; 

        public async Task<Domain.Entities.Comment> Handle(GetCommentByUserName request, CancellationToken cancellationToken)
        {
            var like = await _likeRepository.Query
                .GetQuery(new IncludeLikeCommentSpecification())
                .GetQuery(new LikeCarIdSpecification(request.CarId))
                .GetQuery(new LikeUsernameSpecification(request.UserName))
                .GetQuery(new LikeIsOwnerSpecification())
                .FirstOrDefaultAsync(cancellationToken);

            if (like is null)
            {
                _logger.LogError("Like is null when geting comment");

                throw new NotFoundException(ErrorConstants.LikeNotFoundError);
            }

            if (like.Comment is null)
            {
                _logger.LogError("Comment is null");

                throw new NotFoundException(ErrorConstants.CommentNotFoundError);
            }

            return like.Comment;
        }
    }
}
