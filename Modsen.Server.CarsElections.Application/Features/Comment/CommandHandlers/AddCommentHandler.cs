using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications;
using Modsen.Server.CarsElections.Application.Specifications.UserSpecifications;
using Modsen.Server.CarsElections.Domain.Constants;
using Modsen.Server.CarsElections.Domain.Enums;
using Modsen.Server.CarsElections.Domain.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.Comment.CommandHandlers
{
    public class AddCommentHandler(
        ILikeRepository likeRepository,
        IUserRepository userRepository,
        IMapper mapper) : IRequestHandler<AddComment>
    {
        private readonly ILikeRepository _likeRepository = likeRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(AddComment request, CancellationToken cancellationToken)
        {
            await CheckIsCommentExist(request.CarId, request.UserName, cancellationToken);

            var comment = _mapper.Map<Domain.Entities.Comment>(request);

            var user = await _userRepository.Query
                .GetQuery(new GetUserByUserNameSpecification(request.UserName))
                .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(ErrorConstants.NotFoundUserError);
         
            await _likeRepository.AddAsync(new Domain.Entities.Like
            {
                Comment = comment,
                User = user,
                Type = LikeType.Owner,
            }, cancellationToken);

            await _likeRepository.SaveChangesAsync();
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
                throw new BadRequestException(ErrorConstants.CommentAlreadyExistsError);
            }
        }
    }
}
