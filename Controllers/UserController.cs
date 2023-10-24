using AutoMapper;
using GymApi.Entities;
using GymApi.Models;
using GymApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{id}")]
        public ActionResult<User> Get([FromRoute]int id)
        {
            var user=_userService.GetById(id);

           return Ok(user);
        }

        [HttpPost("register")]
        public ActionResult CreateUser([FromBody] CreateUserDto dto)
        {
           _userService.CreateUser(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] User user, [FromRoute]int id)
        {   
            _userService.UpdateUser(id, user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _userService.DeleteUser(id);

            return NoContent();
        }
    }
}
