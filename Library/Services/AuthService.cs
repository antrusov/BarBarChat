using System;
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
            //...
            return null;
        }

        public User GetUserByAuthToken(string authToken)
        {
            //...
            return null;
        }
    }
}