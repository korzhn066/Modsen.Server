using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsControl.Business.Interfaces;

namespace Modsen.Server.CarsControl.Api.Controllers
{
    [Route("api/election-cars/")]
    [ApiController]
    public class CarsElectionController(IElectionsCarService electionsCarService) : ControllerBase
    {
        private readonly IElectionsCarService _electionsCarService = electionsCarService;

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            await _electionsCarService.DeleteCarAsync(id);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCar(string id, string json)
        {
            await _electionsCarService.UpdateCarAsync(id, json);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCar(string json)
        {
            await _electionsCarService.AddCarAsync(json);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(int page, int count, CancellationToken cancellationToken)
        {
            var cars = await _electionsCarService.GetCarsAsync(page, count, cancellationToken);

            return Ok(cars);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCar(string id, CancellationToken cancellationToken)
        {
            var cars = await _electionsCarService.GetCarAsync(id, cancellationToken);

            return Ok(cars);
        }
    }
}
