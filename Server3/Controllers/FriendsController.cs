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

namespace Server3.Controllers
{
    [ApiController]
    [Route("friends")]
    public class FriendsController : BarBarControllerBase
    {
        public FriendsController(AuthService authService):base(authService)
        {
        }

        //get all
        [HttpGet]
        public ActionResult GetAllFriends()
        {
            CheckAuth();
            return Ok($"FriendsController.GetAllFriends");
        }

        [HttpPost("{id}")]
        public ActionResult AddFriend(string id)
        {
            CheckAuth();
            return Ok($"friend with id={id} was added");
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveFriend(string id)
        {
            CheckAuth();
            return Ok($"friend with id={id} was delete");
        }
   }    
}
