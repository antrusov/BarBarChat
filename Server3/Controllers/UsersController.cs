using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.Models;
using Server3.Models.User;

namespace Server3.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        //users/vasya
        [HttpGet("{id}")]
        public ActionResult<UserVM> GetUser(string id)
        {
            var user = new UserVM() {
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

        [HttpPost]
        public ActionResult AddUser([FromBody] UserCreateVM model)
        {
            return Ok($"User was added!");
        }

    }
}