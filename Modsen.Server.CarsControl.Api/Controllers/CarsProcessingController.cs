using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsControl.Business.Interfaces;

namespace Modsen.Server.CarsControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsProcessingController(IProcessingCarService processingCarService) : ControllerBase
    {
        private readonly IProcessingCarService _processingCarService = processingCarService;

        [HttpPost]
        [Route("remove_car")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            await _processingCarService.DeleteCarAsync(id);

            return NoContent();
        }

        [HttpPost]
        [Route("update_car")]
        public async Task<IActionResult> UpdateCar(string id, string json)
        {
            await _processingCarService.UpdateCarAsync(id, json);

            return NoContent();
        }

        [HttpPost]
        [Route("add_car")]
        public async Task<IActionResult> AddCar(string json)
        {
            await _processingCarService.AddCarAsync(json);

            return NoContent();
        }

        [HttpGet]
        [Route("cars")]
        public async Task<IActionResult> GetCars(int page, int count, CancellationToken cancellationToken)
        {
            var cars = await _processingCarService.GetCarsAsync(page, count, cancellationToken);

            return Ok(cars);
        }

        [HttpGet]
        [Route("car")]
        public async Task<IActionResult> GetCar(string id, CancellationToken cancellationToken)
        {
            var cars = await _processingCarService.GetCarAsync(id, cancellationToken);

            return Ok(cars);
        }
    }
}
