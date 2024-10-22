using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class DesignTemplateFilterDTO
    {
        public string? DefaultLocation { get; set; }
        public string? DefaultShape { get; set; }
        public string? Name {  get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
