﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.UserDTO
{
    public class UserCalculatorPaymentDTO
    {
        public int ProjectPoinPercent { get; set; }
        public int RolePoints { get; set; }
        public int PersonalPoints { get; set; }
        public double BaseSalary { get; set; }
        public double Balance { get; set; }


    }
}
