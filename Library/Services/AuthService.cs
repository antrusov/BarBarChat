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
            if (string.IsNullOrEmpty(authToken))
                return null;
            return _context.Users.Where(user => user.Auth == authToken).FirstOrDefault();
        }

        public string AddAuthToken(User user)
        {
            user.Auth = Guid.NewGuid().ToString();
            _context.SaveChanges();
            return user.Auth;
        }

        public void RemoveAuthToken(User user)
        {
            user.Auth = null;
            _context.SaveChanges();
        }
    }
}