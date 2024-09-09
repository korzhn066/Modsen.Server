﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Comment.Queries;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.Comment.QueryHandlers
{
    public class GetCommentByUserNameHandler(
        ILikeRepository likeRepository,
        ICacheRepository cacheRepository)
        : IRequestHandler<GetCommentByUserName, Domain.Entities.Comment>
    {
        private readonly ILikeRepository _likeRepository = likeRepository;
        private readonly ICacheRepository _cacheRepository = cacheRepository;

        public async Task<Domain.Entities.Comment> Handle(GetCommentByUserName request, CancellationToken cancellationToken)
        {
            var comment = await _cacheRepository.GetAsync<Domain.Entities.Comment>(request.UserName + request.CarId);

            if (comment is not null)
            {
                return comment;
            }

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
