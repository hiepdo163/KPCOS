using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Common
{
    public class QueryPagedServiceAssignment
    {
        public int PageNumber { get; set; } = 1;
        public string? ServiceBookingId { get; set; }
        public string? EmployeeId { get; set; }
        public string? Status { get; set; }
    }
}
