using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.Models;

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
            var user = new User() {
                Id = 111,
                Title = "Админ Рутович",
                Birth = DateTime.Now.AddYears(-20)
            };
            return Ok(user);
        }

        [HttpGet("search/{pattern}")]
        public ActionResult SearchUser(string pattern)
        {
            return Ok($"search result for pattern='{pattern}'");
        }

    }
}