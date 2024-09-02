using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.CommentSpecifications;
using Modsen.Server.CarsElections.Domain.Constants;
using Modsen.Server.CarsElections.Domain.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.Comment.CommandHandlers
{
    public class DeleteCommentHandler(ICommentRepository commentRepository) : IRequestHandler<DeleteComment>
    {
        private readonly ICommentRepository _commentRepository = commentRepository;

        public async Task Handle(DeleteComment request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.Query
                .GetQuery(new CommentIdSpecification(request.CommentId))
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(ErrorConstants.CommentNotFoundError);

            _commentRepository.Delete(comment);

            await _commentRepository.SaveChangesAsync();
        }
    }
}
