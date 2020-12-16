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
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Server3.Models;

namespace Server3.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        readonly AuthService _authService;
        readonly TokenSettings _tokenSettings;
        readonly BarBarContext _context;
        readonly ILogger _logger;

        public UsersController (
            AuthService authService,
            TokenSettings tokenSettings,
            BarBarContext context,
            ILogger<UsersController> logger
        )
        {
            _authService = authService;
            _tokenSettings = tokenSettings;
            _context = context;
            _logger = logger;
        }

        //users/vasya
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<UserVM> GetUser(int id)
        {
            _logger.LogInformation($"GetUser: id={id}");

            var user = _context.Users.Find(id);
            if (user == null)
                return Ok(new { error = $"user width id={id} not found!" });    
            return Ok(user);
        }

        [HttpGet("search/{pattern}")]
        public ActionResult SearchUser(string pattern)
        {
            _logger.LogInformation($"SearchUser: pattern={pattern}");
            var users = _context.Users.Where(user => user.Title.IndexOf(pattern) != -1);
            return Ok(users);
        }

        [HttpPost]
        public ActionResult AddUser([FromBody] UserCreateVM model)
        {
            _logger.LogInformation($"AddUser: Title={model.Title} Login={model.Login} Pass={model.Pass}");

            var user = new User() {
                Title = model.Title,
                Login = model.Login,
                Pass = model.Pass
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login ([FromBody] LoginRequestVM request)
        {
            _logger.LogInformation($"Login: {request}");

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            var user = _authService.GetUserByLoginAndPass(request.UserName, request.Password);
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                _tokenSettings.Issuer,
                _tokenSettings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenSettings.AccessExpiration),
                signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            _logger.LogInformation($"User [{request.UserName}] logged in the system.");
            return Ok(new LoginResultVM
            {
                UserName = request.UserName,
                JwtToken = token
            });
        }

        /*
        [HttpPost("logout")]
        public ActionResult Logout ()
        {            
            _logger.LogInformation($"Logout");
            RemoveAuth();

            Response.Cookies.Append("AuthToken", "");

            return Ok();
        }*/
    }
}