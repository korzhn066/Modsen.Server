namespace Modsen.Server.CarsElections.Domain.Entities
{
    public class Car
    {
        public string Id { get; set; } = null!;

        public virtual List<Comment> Comments { get; set; } = [];
    }
}
