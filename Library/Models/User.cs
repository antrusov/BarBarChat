using System;

namespace Library.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Auth { get; set; }
        public DateTime? LastActivity { get; set; }
    }
}