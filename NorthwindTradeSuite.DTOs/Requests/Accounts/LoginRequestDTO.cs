﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.DTOs.Requests.Accounts
{
    public class LoginRequestDTO
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;   
    }
}
