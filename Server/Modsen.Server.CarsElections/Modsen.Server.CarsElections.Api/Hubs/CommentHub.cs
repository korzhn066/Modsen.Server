using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Modsen.Server.CarsElections.Api.Enums;
using Modsen.Server.CarsElections.Api.Models.Requests;
using Modsen.Server.CarsElections.Api.Models.Responses;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Domain.Entities;
using System.Threading;

namespace Modsen.Server.CarsElections.Api.Hubs
{
    [Authorize]
    public class CommentHub(
        IMapper mapper,
        IMediator mediator) : Hub
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task AddComment(CommentRequest commentRequest)
        {
            var addComment = _mapper.Map<AddComment>(commentRequest);
            addComment.UserName = Context!.User!.Identity!.Name!;

            await _mediator.Send(addComment);

            var response = new CommentHubResponse<Comment>
            {
                Type = CommentHubResponseType.Add,
                Data = _mapper.Map<Comment>(commentRequest)
            };

            await Clients.All.SendAsync("CommentReceive", response);
        }

        public async Task UpdateComment(UpdateComment updateComment)
        {
            var comment = await _mediator.Send(updateComment);

            var response = new CommentHubResponse<Comment>
            {
                Type = CommentHubResponseType.Update,
                Data = comment
            };

            await Clients.All.SendAsync("CommentReceive", response);
        }

        public async Task DeleteComment(int id)
        {
            await _mediator.Send(new DeleteComment
            {
                CommentId = id,
            });

            var response = new CommentHubResponse<int>
            {
                Type = CommentHubResponseType.Add,
                Data = id
            };

            await Clients.All.SendAsync("CommentReceive", response);
        }
    }
}
