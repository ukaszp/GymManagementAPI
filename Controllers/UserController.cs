using AutoMapper;
using AutoMapper.Configuration.Conventions;
using GymApi.Entities;
using GymApi.Models;
using GymApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GymApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        [HttpPost("register")]
        public ActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            _userService.CreateUser(dto);
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {

        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
        public ActionResult<User> Get([FromRoute]int id)
        {
            var user=_userService.GetById(id);

           return Ok(user);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public ActionResult Update([FromBody] User user, [FromRoute]int id)
        {   
            _userService.UpdateUser(id, user);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public ActionResult Delete([FromRoute]int id)
        {
            _userService.DeleteUser(id);

            return NoContent();
        }
        [HttpPost("addmembership/{id}")]
        [Authorize(Roles = "Admin")]

        public ActionResult AddMembership([FromBody] Membership membership, [FromRoute] int id)
        {
            _userService.AddMembership(membership, id);

            return Ok();
        }
        [HttpPut("updatemembership/{id}")]
        [Authorize(Roles = "Admin")]

        public ActionResult UpdateMembership([FromRoute] int userId, [FromBody] Membership membership)
        {
            _userService.UpdateMembership(userId, membership);
            return Ok();
        }
        [HttpPut("{planid},{userid}")]
        [Authorize(Roles = "Admin")]

        public ActionResult PickPlan([FromRoute]int planid, [FromRoute] int userid)
        {
            _userService.PickPlan(planid, userid);
            return Ok(); 
        }
        [HttpPut("assignrole/{roleid}{userid}")]
        [Authorize(Roles = "Admin")]

        public ActionResult AssignRole([FromRoute]int  roleid, [FromRoute]int userid) 
        {
            _userService.AssignRole(roleid, userid);
            return Ok();
        }
    }
}
