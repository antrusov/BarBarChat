using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Library.Models;

namespace Library.Services
{
    public class AuthService
    {
        private readonly BarBarContext _context;
        private readonly ILogger _logger;

        public AuthService (
            BarBarContext context,
            ILogger<AuthService> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public User GetUserByLoginAndPass(string login, string pass)
        {
            return _context.Users.Where(user => user.Login == login && user.Pass == pass).FirstOrDefault();
        }

        public User GetUserByAuthToken(string authToken)
        {
            return _context.Users.Where(user => user.Auth == authToken).FirstOrDefault();
        }
    }
}