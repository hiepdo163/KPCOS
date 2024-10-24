using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class QueryPagedServiceAssignment
    {
        public string? ServiceBookingId { get; set; }
        public string? EmployeeId { get; set; }
        public string? Status { get; set; }
    }
}
