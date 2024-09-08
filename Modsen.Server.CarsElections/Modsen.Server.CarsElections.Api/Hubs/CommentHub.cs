using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Modsen.Server.CarsElections.Api.Models.Requests;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;

namespace Modsen.Server.CarsElections.Api.Hubs
{
    [Authorize]
    public class CommentHub(
        IMapper mapper,
        IMediator mediator) : Hub
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task SendComment(CommentRequest commentRequest)
        {
            var addComment = _mapper.Map<AddComment>(commentRequest);
            addComment.UserName = Context!.User!.Identity!.Name!;

            await _mediator.Send(addComment);

            await Clients.All.SendAsync("CommentReceive", addComment);
        }
    }
}
