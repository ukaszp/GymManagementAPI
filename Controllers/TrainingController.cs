using GymApi.Entities;
using GymApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GymApi.Controllers
{
    [Route("api/training")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet]

        public ActionResult<IEnumerable<Training>> GetAll()
        {
            var trainings = _trainingService.GetAll();

            return Ok(trainings);
        }

        [HttpGet("{id}")]

        public ActionResult<Training> Get([FromRoute] int id)
        {
            var training = _trainingService.GetById(id);

            return Ok(training);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Trainer")]

        public ActionResult CreateTraining([FromBody] Training training)
        {
            var id = _trainingService.CreateTraining(training);
            return Created($"/api/training/{training.Id}", null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Trainer")]

        public ActionResult Update([FromBody] Training training, [FromRoute] int id)
        {
            _trainingService.UpdateTraining(id, training);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Trainer")]

        public ActionResult Delete([FromRoute] int id)
        {
            _trainingService.DeleteTraining(id);

            return NoContent();
        }
    }
}

