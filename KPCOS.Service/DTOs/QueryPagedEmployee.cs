using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.DTOs
{
    public class QueryPagedEmployee
    {
        public int PageNumber { get; set; } = 1;

        public string? Name { get; set; }

        public decimal? FromSalary { get; set; }

        public decimal? ToSalary { get; set; }
    }
}
