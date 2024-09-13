using Microsoft.AspNetCore.SignalR;

namespace Modsen.Server.CarsElections.Api.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public virtual string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
        }
    }
}
