using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Modsen.Server.CarsControl.Business.Models.Requests
{
    public class AddCar
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Json { get; set; } = null!;
        public IFormFileCollection FormFiles { get; set; } = new FormFileCollection();
    }
}
