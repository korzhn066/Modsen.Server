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
    public class UpdateCommentHandler(
        ICommentRepository commentRepository,
        ILogger<UpdateCommentHandler> logger) : IRequestHandler<UpdateComment>
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly ILogger<UpdateCommentHandler> _logger = logger;

        public async Task Handle(UpdateComment request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.Query
                .GetQuery(new CommentIdSpecification(request.CommentId))
                .FirstOrDefaultAsync(cancellationToken);

            if (comment is null)
            {
                _logger.LogError("Comment is null when updating");

                throw new NotFoundException(ErrorConstants.CommentNotFoundError);
            }

            comment.Message = request.Message;

            await _commentRepository.SaveChangesAsync();

            _logger.LogInformation("Update comment");
        }
    }
}
