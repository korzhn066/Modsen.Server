using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsControl.Business.Interfaces;
using Modsen.Server.CarsControl.Business.Models.Requests;
using Modsen.Server.CarsControl.DataAccess.Models;

namespace Modsen.Server.CarsControl.Api.Controllers
{
    [Route("api/cars/rent/")]
    [ApiController]
    public class CarsRentController(IRentCarService rentCarService) : ControllerBase
    {
        private readonly IRentCarService _rentCarService = rentCarService;

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            await _rentCarService.DeleteCarAsync(id);

            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCar(UpdateCar updateCar)
        {
            await _rentCarService.UpdateCarAsync(updateCar);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCar(AddCar addCar)
        {
            await _rentCarService.AddCarAsync(addCar);

            return NoContent();
        }

        [HttpPost]
        [Route("move")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Move(MoveCar moveCar)
        {
            await _rentCarService.MoveAsync(moveCar);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(int page, int count, CancellationToken cancellationToken)
        {
            var cars = await _rentCarService.GetCarsAsync(page, count, cancellationToken);

            return Ok(cars);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCar(string id, CancellationToken cancellationToken)
        {
            var cars = await _rentCarService.GetCarAsync(id, cancellationToken);

            return Ok(cars);
        }
    }
}
