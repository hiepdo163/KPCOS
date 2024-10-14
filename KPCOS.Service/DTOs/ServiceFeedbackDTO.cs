using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class ServiceFeedbackDTO
    {
        public string ID {  get; set; }
        public string ServiceBookingId { get; set; }
        public string CustomerId { get; set; }
        public decimal? Rating { get; set; }
        public string Feedback { get; set; }
    }
}
