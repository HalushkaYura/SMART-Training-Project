using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.UserDTO
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDay { get; set; }
    }
}
