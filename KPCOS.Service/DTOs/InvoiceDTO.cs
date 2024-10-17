using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class InvoiceDTO
    {
        public string Id { get; set; }

        public decimal? DiscountApplied { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string PaymentMethod { get; set; }

        public string ProjectId { get; set; }

        public string Status { get; set; }

        public decimal? TaxAmount { get; set; }

        public decimal? TotalAmount { get; set; }
    }
}
