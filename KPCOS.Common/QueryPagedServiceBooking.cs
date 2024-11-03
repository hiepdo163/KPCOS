using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Common
{
    public class QueryPagedServiceBooking
    {
        public int PageNumber { get; set; } = 1;
        public string? ServiceType { get; set; }
        public string? CustomerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }
    }
}
