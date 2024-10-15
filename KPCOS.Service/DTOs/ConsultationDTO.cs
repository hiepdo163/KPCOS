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
        public string Id { get; set; } = null!;
        public string DesignId { get; set; } = null!;

        public string AdjustedDesign { get; set; } = null!;

        public string AdjustedSpecification { get; set; } = null!;

        public string Note { get; set; } = null!;
    }
}
