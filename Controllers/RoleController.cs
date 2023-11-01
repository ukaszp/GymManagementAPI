using GymApi.Entities;
using GymApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GymApi.Controllers
{
        [Route("api/role")]
        [ApiController]
        public class RoleController : ControllerBase
        {
            private readonly IRoleService _roleService;

            public RoleController(IRoleService roleService)
            {
            _roleService = roleService;
        }

            [HttpGet]
        [Authorize(Roles = "Admin")]

        public ActionResult<IEnumerable<Role>> GetAll()
            {
                var roles = _roleService.GetAll();

                return Ok(roles);
            }


            [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public ActionResult<Role> Get([FromRoute] int id)
            {
                var role = _roleService.GetById(id);

                return Ok(role);
            }


            [HttpPost]
        [Authorize(Roles = "Admin")]

        public ActionResult CreateRole([FromBody] Role role)
            {
                var id = _roleService.CreateRole(role);
                return Created($"/api/role/{role.Id}", null);
            }

            [HttpPut("{id}")]
            public ActionResult Update([FromBody] Role role, [FromRoute] int id)
            {
                _roleService.UpdateRole(id, role);

                return Ok();
            }

            [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public ActionResult Delete([FromRoute] int id)
            {
                _roleService.DeleteRole(id);

                return NoContent();
            }
        }
}
