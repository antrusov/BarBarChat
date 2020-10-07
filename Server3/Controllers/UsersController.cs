using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server3.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        //users/vasya
        [HttpGet("{id}")]
        public ActionResult GetUser(string id)
        {
            return Ok($"get 1");
        }

        [HttpGet("search/{pattern}")]
        public ActionResult SearchUser(string pattern)
        {
            return Ok($"search result for pattern='{pattern}'");
        }

    }
}