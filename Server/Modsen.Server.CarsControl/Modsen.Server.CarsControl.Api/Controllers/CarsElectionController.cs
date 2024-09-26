using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsControl.Business.Models.Requests;
using Modsen.Server.CarsControl.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Modsen.Server.CarsControl.DataAccess.Models;

namespace Modsen.Server.CarsControl.Api.Controllers
{
    [Route("api/cars/election/")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCar(UpdateCar updateCar)
        {
            await _electionsCarService.UpdateCarAsync(updateCar);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCar(AddCar addCar)
        {
            await _electionsCarService.AddCarAsync(addCar);

            return NoContent();
        }

        [HttpPost]
        [Route("move")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Move(MoveCar moveCar)
        {
            await _electionsCarService.MoveAsync(moveCar);

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
            var car = await _electionsCarService.GetCarAsync(id, cancellationToken);

            return Ok(car);
        }
    }
}
