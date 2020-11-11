using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.Models;
using Library.Services;
using Server3.Models.User;

namespace Server3.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly BarBarContext _context;
        private readonly ILogger _logger;
        private readonly AuthService _authService;

        public UsersController (
            BarBarContext context,
            ILogger<UsersController> logger,
            AuthService authService
        )
        {
            _context = context;
            _logger = logger;
            _authService = authService;
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
            _logger.LogInformation($"AddUser: Title={model.Title} Login={model.Login} Pass={model.Pass}");
            try
            {
                var user = new User() {
                    Title = model.Title,
                    Login = model.Login,
                    Pass = model.Pass
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

        [HttpPost("login")]
        public ActionResult Login (string login, string pass)
        {            
            _logger.LogInformation($"Login: login={login} pass={pass}");
            try
            {
                var user = _authService.GetUserByLoginAndPass(login, pass);
                //...
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