using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Logic.Commands
{
    public class DeleteMarketLeadCommand : IAuditCommand, IRequest<CommonCommandResult>
    {
        [JsonIgnore]
        public string UserName { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
