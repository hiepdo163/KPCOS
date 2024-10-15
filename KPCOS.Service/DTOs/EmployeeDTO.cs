using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class EmployeeDTO
    {
        public decimal? Salary { get; set; }

        public string? SupervisorId { get; set; }

        public string? UserId { get; set; }
    }
}
