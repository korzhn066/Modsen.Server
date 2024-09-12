using Modsen.Server.CarsElections.Api.Enums;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Api.Models.Responses
{
    public class CommentHubResponse<T>
    {
        public CommentHubResponseType Type { get; set; }
        public T Data { get; set; } = default!;
    }
}
