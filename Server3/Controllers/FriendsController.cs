using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server3.Controllers
{
    [ApiController]
    [Route("friends")]
    public class FriendsController : ControllerBase
    {
        //get all
        [HttpGet]
        public ActionResult GetAllFriends()
        {
            return Ok($"FriendsController.GetAllFriends");
        }

        [HttpPost("{id}")]
        public ActionResult AddFriend(string id)
        {
            return Ok($"friend with id={id} was added");
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveFriend(string id)
        {
            return Ok($"friend with id={id} was delete");
        }
   }    
}
