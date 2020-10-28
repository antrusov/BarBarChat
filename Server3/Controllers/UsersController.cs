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
        }

        //users/vasya
        [HttpGet("{id}")]
        public ActionResult<UserVM> GetUser(int id)
        {
            _logger.LogInformation($"GetUser: id={id}");
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    return Ok(new { error = $"user width id={id} not found!" });    
                return Ok(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new { error = ex.Message });
            }
        }

        [HttpGet("search/{pattern}")]
        public ActionResult SearchUser(string pattern)
        {
            _logger.LogInformation($"SearchUser: pattern={pattern}");
            try
            {
                var users = _context.Users.Where(user => user.Title.IndexOf(pattern) != -1);
                return Ok(users);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new { error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult AddUser([FromBody] UserCreateVM model)
        {
            _logger.LogInformation($"AddUser: Title={model.Title} Birth={model.Birth}");
            try
            {
                var user = new User() {
                    Title = model.Title,
                    Birth = model.Birth
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return Ok(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new { error = ex.Message });
            }
        }

    }
}