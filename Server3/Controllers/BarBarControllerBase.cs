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
    public class BarBarControllerBase : ControllerBase
    {
        protected readonly AuthService _authService;

        public BarBarControllerBase(AuthService authService)
        {
            _authService = authService;
        }

        protected void CheckAuth()
        {
            var authToken = Request.Cookies["AuthToken"];
            var user = _authService.GetUserByAuthToken(authToken);
            if (user == null)
                throw new Exception("Access denied!!!");
        }

        protected void RemoveAuth()
        {
            var authToken = Request.Cookies["AuthToken"];
            var user = _authService.GetUserByAuthToken(authToken);
            if (user != null)
                _authService.RemoveAuthToken(user);
        }
}    
}