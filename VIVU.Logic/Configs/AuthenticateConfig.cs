using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Logic.Configs
{
    public class AuthenticateConfig
    {
        public static string ConfigName { get; set; } = "Authenticate";
        public string Issuer { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public int TokenExpireAfterMinutes { get; set; } = 0;
        public string DefaultRedirect { get; set; } = string.Empty;
    }
}
