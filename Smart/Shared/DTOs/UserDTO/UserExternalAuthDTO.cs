﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.UserDTO
{
    public class UserExternalAuthDTO
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
    }
}
