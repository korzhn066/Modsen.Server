using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.CommentSpecifications;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.Comment.CommandHandlers
{
    public class UpdateCommentHandler(ICommentRepository commentRepository) : IRequestHandler<UpdateComment>
    {
        private readonly ICommentRepository _commentRepository = commentRepository;

        public async Task Handle(UpdateComment request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.Query
                .GetQuery(new CommentIdSpecification(request.CommentId))
                .FirstOrDefaultAsync(cancellationToken) 
                ?? throw new NotFoundException(ErrorConstants.CommentNotFoundError);

            comment.Message = request.Message;

            await _commentRepository.SaveChangesAsync();
        }
    }
}
