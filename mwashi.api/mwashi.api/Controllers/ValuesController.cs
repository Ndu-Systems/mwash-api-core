using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mwashi.api.Contracts;
using mwashi.api.Data;
using mwashi.api.Data.Entities;

namespace mwashi.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_userService.GetAllUsers());
        }

 
    }
}
