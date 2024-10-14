using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class ServiceExecutionDTO
    {
        public string ServiceBookingId { get; set; } = null!;

        public string EmployeeId { get; set; } = null!;

        public DateTime? ExecutionDate { get; set; }

        public string Result { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
