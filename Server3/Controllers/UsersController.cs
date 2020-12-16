using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.Models;
using Library.Services;
using Server3.Models.User;
using Server3.Infrastructure.Jwt;

namespace Server3.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : BarBarControllerBase
    {
        private readonly TokenSettings _tokenSettings;
        private readonly BarBarContext _context;
        private readonly ILogger _logger;

        public UsersController (
            TokenSettings tokenSettings,
            BarBarContext context,
            ILogger<UsersController> logger,
            AuthService authService
        ) : base(authService)
        {
            _tokenSettings = tokenSettings;
            _context = context;
            _logger = logger;
        }

        //users/vasya
        [HttpGet("{id}")]
        public ActionResult<UserVM> GetUser(int id)
        {
            _logger.LogInformation($"GetUser: id={id}");
            CheckAuth();

            var user = _context.Users.Find(id);
            if (user == null)
                return Ok(new { error = $"user width id={id} not found!" });    
            return Ok(user);
        }

        [HttpGet("search/{pattern}")]
        public ActionResult SearchUser(string pattern)
        {
            _logger.LogInformation($"SearchUser: pattern={pattern}");
            CheckAuth();
            var users = _context.Users.Where(user => user.Title.IndexOf(pattern) != -1);
            return Ok(users);
        }

        [HttpPost]
        public ActionResult AddUser([FromBody] UserCreateVM model)
        {
            _logger.LogInformation($"AddUser: Title={model.Title} Login={model.Login} Pass={model.Pass}");
            CheckAuth();

            var user = new User() {
                Title = model.Title,
                Login = model.Login,
                Pass = model.Pass
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult Login (string login, string pass)
        {            
            _logger.LogInformation($"Login: login={login} pass={pass}");
            var user = _authService.GetUserByLoginAndPass(login, pass);
            if (user == null)
                return Ok(new { error = "Неправильный логин/пароль." });
            
            var authToken = _authService.AddAuthToken(user);

            Response.Cookies.Append("AuthToken", authToken);

            return Ok();
        }

        [HttpPost("logout")]
        public ActionResult Logout ()
        {            
            _logger.LogInformation($"Logout");
            RemoveAuth();

            Response.Cookies.Append("AuthToken", "");

            return Ok();
        }
    }
}