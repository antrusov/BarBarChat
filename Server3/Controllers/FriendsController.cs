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
        [HttpGet]
        public ActionResult Add(string id)
        {
            return Ok($"friend with id {id} added!");
        }
        //find
        //add
        //del
        //get 1
        //get all
    }    
}