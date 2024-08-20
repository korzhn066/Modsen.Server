using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsControl.Business.UseCases.Rent;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Repository;
using MongoDB.Bson;

//C:\mongodb\bin\mongod

namespace Modsen.Server.CarsControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CarsRentController : ControllerBase
    {
        private readonly GetCarsFromRentUseCase _getCarsFromRentUseCase;
        private readonly AddCarToRentUseCase _addCarToRentUseCase;
        private readonly UpdateCarFromRentUseCase _updateCarFromRentUseCase;
        private readonly RemoveCarFromRentUseCase _removeCarFromRentUseCase;

        public CarsRentController(
            GetCarsFromRentUseCase getCarsFromRentUseCase,
            AddCarToRentUseCase addCarToRentUseCase,
            UpdateCarFromRentUseCase updateCarFromRentUseCase,
            RemoveCarFromRentUseCase removeCarFromRentUseCase)
        {
            _addCarToRentUseCase = addCarToRentUseCase;
            _getCarsFromRentUseCase = getCarsFromRentUseCase;
            _updateCarFromRentUseCase = updateCarFromRentUseCase;
            _removeCarFromRentUseCase = removeCarFromRentUseCase;
        }

        [HttpPost]
        [Route("remove_car")]
        public async Task<IActionResult> RemoveCar(string id)
        {
            await _removeCarFromRentUseCase.RemoveCarAsync(id);

            return NoContent();
        }

        [HttpPost]
        [Route("update_car")]
        public async Task<IActionResult> UpdateCar(string id, string json)
        {
            await _updateCarFromRentUseCase.UpdateCarAsync(id, BsonDocument.Parse(json));

            return NoContent();
        }

        [HttpPost]
        [Route("add_car")]
        public async Task<IActionResult> AddCar(string json)
        {
            await _addCarToRentUseCase.AddCarAsync(BsonDocument.Parse(json));

            return NoContent();
        }

        [HttpGet]
        [Route("cars")]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _getCarsFromRentUseCase.GetCarsAsync();

            return Ok(new
            {
                cars = cars
            });
        }
    }
}
