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
        public ActionResult<IEnumerable<Role>> GetAll()
            {
                var roles = _roleService.GetAll();

                return Ok(roles);
            }


        [HttpGet("{id}")]

        public ActionResult<Role> Get([FromRoute] int id)
            {
                var role = _roleService.GetById(id);

                return Ok(role);
            }


         [HttpPost]

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
        public ActionResult Delete([FromRoute] int id)
             {
               _roleService.DeleteRole(id);

               return NoContent();
             }
        }
}
