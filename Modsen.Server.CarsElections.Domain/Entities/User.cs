namespace Modsen.Server.CarsElections.Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;

        public virtual List<Like> Likes { get; set; } = new();
    }
}
