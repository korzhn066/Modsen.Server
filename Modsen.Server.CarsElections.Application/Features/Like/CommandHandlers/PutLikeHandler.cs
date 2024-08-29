using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Like.Command;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.CommentSpecifications;
using Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications;
using Modsen.Server.CarsElections.Application.Specifications.UserSpecifications;
using Modsen.Server.CarsElections.Domain.Constants;
using Modsen.Server.CarsElections.Domain.Enums;
using Modsen.Server.CarsElections.Domain.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.Like.CommandHandlers
{
    public class PutLikeHandler(
        ILikeRepository likeRepository,
        ICommentRepository commentRepository,
        IUserRepository userRepository) : IRequestHandler<PutLike>
    {
        private readonly ILikeRepository _likeRepository = likeRepository;
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task Handle(PutLike request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Query
                .GetQuery(new UserUserNameSpecification(request.UserName))
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(ErrorConstants.NotFoundUserError);

            var comment = await _commentRepository.Query
                .GetQuery(new CommentIdSpecification(request.CommentId))
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(ErrorConstants.CommentNotFoundError);

            var like = await _likeRepository.Query
                .GetQuery(new LikeUsernameSpecification(request.UserName))
                .GetQuery(new LikeCommentIdSpecification(request.CommentId))
                .FirstOrDefaultAsync(cancellationToken);

            if (like is null)
            {
                ChangeLikeCount(comment, LikeType.Like);

                await _likeRepository.AddAsync(new Domain.Entities.Like
                {
                    Comment = comment,
                    User = user,
                    Type = request.LikeType
                }, cancellationToken);
            }
            else
            {
                if (like.Type == request.LikeType)
                {
                    throw new BadRequestException(ErrorConstants.LikeAlreadyExistsError);
                }

                like.Type = request.LikeType;
                ChangeExistLikeCount(comment, LikeType.Like);
            }

            await _likeRepository.SaveChangesAsync();
        }

        private static Domain.Entities.Comment ChangeLikeCount(Domain.Entities.Comment comment, LikeType likeType)
        {
            switch (likeType)
            {
                case LikeType.Like:
                    comment.LikeCount++;
                    break;
                case LikeType.Dislike:
                    comment.DislikeCount++;
                    break;
            }

            return comment;
        }

        private static Domain.Entities.Comment ChangeExistLikeCount(Domain.Entities.Comment comment, LikeType likeType)
        {
            switch (likeType)
            {
                case LikeType.Like:
                    comment.LikeCount++;
                    comment.DislikeCount--;
                    break;
                case LikeType.Dislike:
                    comment.DislikeCount++;
                    comment.LikeCount--;
                    break;
            }

            return comment;
        }
    }
}