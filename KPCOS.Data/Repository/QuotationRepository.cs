﻿using KPCOS.Data.Base;
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

        public IQueryable<Quotation> GetQuery() => _context.Quotations.AsQueryable();
    }
}
