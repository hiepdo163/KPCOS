using Azure.Core;
using KPCOS.Common;
using KPCOS.Data.Base;
using KPCOS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Data.Repository
{
    public class QuotationRepository : GenericRepository<Quotation>
    {
        public QuotationRepository() { }

        public QuotationRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;

        public async Task<List<Quotation>> GetAllFilter(QuotationFilterParams request)
        {
            var quotations = _context.Quotations.AsQueryable();

            if (!string.IsNullOrEmpty(request.ComplexityLevel))
            {
                quotations = quotations.Where(q => q.ComplexityLevel.Contains(request.ComplexityLevel));
            }

            if (!string.IsNullOrEmpty(request.Status))
            {
                quotations = quotations.Where(q => q.Status == request.Status);
            }

            if (request.StartDate.HasValue)
            {
                quotations = quotations.Where(q => q.QuotationDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                quotations = quotations.Where(q => q.QuotationDate <= request.EndDate.Value);
            }

            // Check if no quotations match the filters
            if (quotations == null || !quotations.Any())
            {
               return new List<Quotation>();
            }
            return await quotations.ToListAsync();
        }

        public IQueryable<Quotation> GetQuery() => _context.Quotations.AsQueryable();
    }
}
