using System;

namespace Server3.Models.User
{
    public class UserCreateVM
    {
        public string Title { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
    }
}