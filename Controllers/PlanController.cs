using GymApi.Entities;
using GymApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GymApi.Controllers
{
        [Route("api/plans")]
        [ApiController]
        public class PlanController : ControllerBase
        {
            private readonly IPlanService _planService;

            public PlanController(IPlanService planService)
            {
               _planService = planService;
            }

            [HttpGet]
            public ActionResult<IEnumerable<Plan>> GetAll()
            {
                var plans = _planService.GetAll();

                return Ok(plans);
            }

            [HttpGet("{id}")]
            public ActionResult<Plan> Get([FromRoute] int id)
            {
                var plan = _planService.GetById(id);
                return Ok(plan);
            }

            [HttpPost]
            public ActionResult CreatePlan([FromBody] Plan plan)
            {
                var id = _planService.CreatePlan(plan);
                return Created($"/api/plan/{plan.Id}", null);
            }

            [HttpPut("{id}")]
          public ActionResult Update([FromBody] Plan plan, [FromRoute] int id)
            {
            _planService.CreatePlan(plan);

                return Ok();
            }

            [HttpDelete("{id}")]
            public ActionResult Delete([FromRoute] int id)
            {
            _planService.DeletePlan(id);

                return NoContent();
            }
            [HttpPost("addMembership")]
            public ActionResult AddMembership([FromBody] Membership membership, int planId)
            {
                _planService.AddMembership(membership, planId);
                return NoContent();
            }
           [HttpGet("memberships")]
            public ActionResult GetMemberships()
            {
                _planService.GetMemberships();
                return NoContent();
            }
            
        }
    }

