namespace Modsen.Server.CarsControl.DataAccess.Models
{
    public class UpdateCar
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Json { get; set; } = null!;
    }
}
