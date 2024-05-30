﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.UserDTO
{
    public class UserAutorizationDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Provider { get; set; }
        public bool Is2StepVerificationRequired { get; set; }
    }
}
