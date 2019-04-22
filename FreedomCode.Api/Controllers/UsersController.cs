using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreedomCode.Api.Contracts;
using FreedomCode.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreedomCode.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        //POST: api/users/authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.AuthenticateUser(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Invalid Email or Password" }); // can be described in an enum ValidationEnum?

            return Ok(user);
        }
        // GET: api/users
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult Get()
        {
            var userList = _userService.GetAllUsers();
            if (userList == null) return NotFound(new { message = "No users returned contact system administrator" });

            return Ok(userList);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();

            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if(id != currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }

            return Ok(user);
        }        
    }
}
