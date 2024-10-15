using System;

namespace KPCOS.Data.Models
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public decimal? ActualCost { get; set; }
        public string ConstructionStaffId { get; set; }
        public string CustomerId { get; set; }
        public string DesignerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string Status { get; set; }
    }
}
