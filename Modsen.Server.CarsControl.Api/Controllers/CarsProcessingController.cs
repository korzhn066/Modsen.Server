using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsControl.Business.UseCases.Processing;
using MongoDB.Bson;

//C:\mongodb\bin\mongod

namespace Modsen.Server.CarsControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CarsProcessingController : ControllerBase
    {
        private readonly GetCarsFromProcessingUseCase _getCarsFromProcessingUseCase;
        private readonly AddCarToProcessingUseCase _addCarToProcessingUseCase;
        private readonly UpdateCarFromProcessingUseCase _updateCarFromProcessingUseCase;
        private readonly RemoveCarFromProcessingUseCase _removeCarFromProcessingUseCase;

        public CarsProcessingController(
            GetCarsFromProcessingUseCase getCarsFromProcessingUseCase,
            AddCarToProcessingUseCase addCarToProcessingUseCase,
            UpdateCarFromProcessingUseCase updateCarFromProcessingUseCase,
            RemoveCarFromProcessingUseCase removeCarFromProcessingUseCase)
        {
            _addCarToProcessingUseCase = addCarToProcessingUseCase;
            _getCarsFromProcessingUseCase = getCarsFromProcessingUseCase;
            _updateCarFromProcessingUseCase = updateCarFromProcessingUseCase;
            _removeCarFromProcessingUseCase = removeCarFromProcessingUseCase;
        }

        [HttpPost]
        [Route("remove_car")]
        public async Task<IActionResult> RemoveCar(string id)
        {
            await _removeCarFromProcessingUseCase.RemoveCarAsync(id);

            return NoContent();
        }

        [HttpPost]
        [Route("update_car")]
        public async Task<IActionResult> UpdateCar(string id, string json)
        {
            await _updateCarFromProcessingUseCase.UpdateCarAsync(id, BsonDocument.Parse(json));

            return NoContent();
        }

        [HttpPost]
        [Route("add_car")]
        public async Task<IActionResult> AddCar(string json)
        {
            await _addCarToProcessingUseCase.AddCarAsync(BsonDocument.Parse(json));

            return NoContent();
        }

        [HttpGet]
        [Route("cars")]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _getCarsFromProcessingUseCase.GetCarsAsync();

            return Ok(new
            {
                cars = cars
            });
        }
    }
}
