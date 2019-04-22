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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAllUsers());
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userService.GetUserById(id));
        }        
    }
}
