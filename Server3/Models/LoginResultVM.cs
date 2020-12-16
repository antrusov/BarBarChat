using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Server3.Models
{
    public class LoginResultVM
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>admin</example>
        public string UserName { get; set; }
        public string JwtToken { get; set; }
    }

}