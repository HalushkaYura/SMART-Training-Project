using System.Globalization;

namespace Smart.Shared.DTOs.UserDTO
{
    public class UserInfoDTO
    {
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
