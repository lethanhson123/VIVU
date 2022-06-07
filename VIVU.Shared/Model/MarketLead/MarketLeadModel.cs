using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class MarketLeadModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;     
        public string Channel { get; set; } = string.Empty;
        public string ReferenceId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
    }
}
