using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class ServiceBookingDTO
    {
        public string CustomerId { get; set; } = null!;

        public string ServiceType { get; set; } = null!;

        public string ScheduleType { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Status { get; set; } = null!;

        public string Note { get; set; } = null!;
    }
}
