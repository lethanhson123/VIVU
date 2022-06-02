using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Shared.Absstraction;

namespace VIVU.Logic.Commands
{
    public class DeleteBannerCommand : IAuditCommand, IRequest<CommonCommandResult>
    {
        public string UserName { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
