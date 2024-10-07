using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsControl.Business.Interfaces;
using Modsen.Server.CarsControl.Business.Models.Requests;
using Modsen.Server.CarsControl.DataAccess.Models;

namespace Modsen.Server.CarsControl.Api.Controllers
{
    [Route("api/cars/processing/")]
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
        public async Task<IActionResult> UpdateCar(UpdateCar updateCar)
        {
            await _processingCarService.UpdateCarAsync(updateCar);

            return NoContent();
        }

        [HttpPost]
        [Route("move")]
        public async Task<IActionResult> Move(MoveCar moveCar)
        {
            await _processingCarService.MoveAsync(moveCar);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(AddCar addCar)
        {
            await _processingCarService.AddCarAsync(addCar);

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
