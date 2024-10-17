using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class FeedbackDTO
    {
        public string ID { get; set; }
        public string CustomerId { get; set; }
        public string ProjectId { get; set; }
        public string Content { get; set; }
        public decimal? Rating { get; set; }
    }
}
