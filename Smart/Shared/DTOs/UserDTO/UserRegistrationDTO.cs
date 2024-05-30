using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.UserDTO
{
    public class UserRegistrationDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
