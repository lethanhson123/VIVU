using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Data.Entities
{
    public class UserToken : CommonAudit
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }
        public bool IsExpired { get; set; }
    }
}
