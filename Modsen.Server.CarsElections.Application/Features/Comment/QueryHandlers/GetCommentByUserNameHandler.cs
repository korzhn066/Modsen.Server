using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Comment.Queries;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications;
using Modsen.Server.CarsElections.Domain.Constants;
using Modsen.Server.CarsElections.Domain.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.Comment.QueryHandlers
{
    public class GetCommentByUserNameHandler(ILikeRepository likeRepository)
        : IRequestHandler<GetCommentByUserName, Domain.Entities.Comment>
    {
        private readonly ILikeRepository _likeRepository = likeRepository;
        
        public async Task<Domain.Entities.Comment> Handle(GetCommentByUserName request, CancellationToken cancellationToken)
        {
            var like = await _likeRepository.Query
                .GetQuery(new IncludeLikeCommentSpecification())
                .GetQuery(new LikeCarIdSpecification(request.CarId))
                .GetQuery(new LikeUsernameSpecification(request.UserName))
                .GetQuery(new LikeIsOwnerSpecification())
                .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(ErrorConstants.LikeNotFoundError);

            return like.Comment ?? throw new NotFoundException(ErrorConstants.CommentNotFoundError);
        }
    }
}
