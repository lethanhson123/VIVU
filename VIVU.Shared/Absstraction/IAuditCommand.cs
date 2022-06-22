using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Absstraction
{
    public interface IAuditCommand
    {
        string UserName { get; set; }
    }
}
