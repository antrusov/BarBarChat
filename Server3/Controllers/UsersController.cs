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
        private readonly BarBarContext _context;
        private readonly ILogger _logger;

        public UsersController (
            BarBarContext context,
            ILogger<UsersController> logger
        )
        {
            _context = context;
            _logger = logger;

            if (_context.Users.Count() == 0)             
            {                 
                _context.Users.Add(new User { Title = "first user", Birth = DateTime.UtcNow });
                _context.SaveChanges();             
            }  
        }

        //users/vasya
        [HttpGet("{id}")]
        public ActionResult<UserVM> GetUser(string id)
        {
            _logger.LogInformation("GetUser");
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
            _logger.LogInformation("SearchUser");
            return Ok($"search result for pattern='{pattern}'");
        }

        [HttpPost]
        public ActionResult AddUser([FromBody] UserCreateVM model)
        {
            _logger.LogInformation("AddUser");
            return Ok($"User was added!");
        }

    }
}