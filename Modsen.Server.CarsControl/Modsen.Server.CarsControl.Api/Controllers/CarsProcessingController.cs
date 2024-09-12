using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsControl.Business.Interfaces;

namespace Modsen.Server.CarsControl.Api.Controllers
{
    [Route("api/processing-cars/")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CarsProcessingController(IProcessingCarService processingCarService) : ControllerBase
    {
        private readonly IProcessingCarService _processingCarService = processingCarService;

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            await _processingCarService.DeleteCarAsync(id);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCar(string id, string json)
        {
            await _processingCarService.UpdateCarAsync(id, json);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(string json, IFormFileCollection formFiles)
        {
            await _processingCarService.AddCarAsync(json, formFiles);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(int page, int count, CancellationToken cancellationToken)
        {
            var cars = await _processingCarService.GetCarsAsync(page, count, cancellationToken);

            return Ok(cars);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCar(string id, CancellationToken cancellationToken)
        {
            var cars = await _processingCarService.GetCarAsync(id, cancellationToken);

            return Ok(cars);
        }
    }
}
