using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.CommentSpecifications;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Modsen.Server.CarsElections.Application.Features.Comment.CommandHandlers
{
    public class DeleteCommentHandler(
        ICommentRepository commentRepository,
        ILogger<DeleteCommentHandler> logger) : IRequestHandler<DeleteComment>
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly ILogger<DeleteCommentHandler> _logger = logger;

        public async Task Handle(DeleteComment request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.Query
                .GetQuery(new CommentIdSpecification(request.CommentId))
                .FirstOrDefaultAsync(cancellationToken);

            if (comment is null)
            {
                _logger.LogError("Comment is null when deliting");

                throw new NotFoundException(ErrorConstants.CommentNotFoundError);
            }

            _commentRepository.Delete(comment);

            await _commentRepository.SaveChangesAsync();

            _logger.LogInformation("Delete comment");
        }
    }
}
