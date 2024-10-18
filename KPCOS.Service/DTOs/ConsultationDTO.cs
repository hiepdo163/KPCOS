using KPCOS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class ConsultationDTO
    {
        public required string DesignId { get; set; } = null!;

        public required string AdjustedDesign { get; set; } = null!;

        public required string AdjustedSpecification { get; set; } = null!;

        public required string Note { get; set; } = null!;
    }
}
