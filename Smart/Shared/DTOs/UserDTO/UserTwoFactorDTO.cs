﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.UserDTO
{
    public class UserTwoFactorDTO
    {
        public string Email { get; set; }

        public string Provider { get; set; }

        public string Token { get; set; }
    }
}
