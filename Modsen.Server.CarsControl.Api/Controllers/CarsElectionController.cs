using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsControl.Business.UseCases.Election;
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
    public class CarsElectionController : ControllerBase
    {
        private readonly GetCarsFromElectionUseCase _getCarsFromElectionUseCase;
        private readonly AddCarToElectionUseCase _addCarToElectionUseCase;
        private readonly UpdateCarFromElectionUseCase _updateCarFromElectionUseCase;
        private readonly RemoveCarFromElectionUseCase _removeCarFromElectionUseCase;

        public CarsElectionController(
            GetCarsFromElectionUseCase getCarsFromElectionUseCase,
            AddCarToElectionUseCase addCarToElectionUseCase,
            UpdateCarFromElectionUseCase updateCarFromElectionUseCase,
            RemoveCarFromElectionUseCase removeCarFromElectionUseCase)
        {
            _addCarToElectionUseCase = addCarToElectionUseCase;
            _getCarsFromElectionUseCase = getCarsFromElectionUseCase;
            _updateCarFromElectionUseCase = updateCarFromElectionUseCase;
            _removeCarFromElectionUseCase = removeCarFromElectionUseCase;
        }

        [HttpPost]
        [Route("remove_car")]
        public async Task<IActionResult> RemoveCar(string id)
        {
            await _removeCarFromElectionUseCase.RemoveCarAsync(id);

            return NoContent();
        }

        [HttpPost]
        [Route("update_car")]
        public async Task<IActionResult> UpdateCar(string id, string json)
        {
            await _updateCarFromElectionUseCase.UpdateCarAsync(id, BsonDocument.Parse(json));

            return NoContent();
        }

        [HttpPost]
        [Route("add_car")]
        public async Task<IActionResult> AddCar(string json)
        {
            await _addCarToElectionUseCase.AddCarAsync(BsonDocument.Parse(json));

            return NoContent();
        }

        [HttpGet]
        [Route("cars")]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _getCarsFromElectionUseCase.GetCarsAsync();

            return Ok(new
            {
                cars = cars
            });
        }
    }
}
