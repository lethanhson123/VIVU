﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Logic.Configs
{
    public class AuthenticateConfig
    {
        public static string ConfigName { get; set; } = "Authenticate";
        public int? TokenExpireAfterMinutes { get; set; }
        public string SecretKey { get; set; } =string.Empty;
        public string VerifySignUp { get; set; } = string.Empty;
    }
}
