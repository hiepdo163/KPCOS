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
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository() { }

        public CustomerRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.AsNoTracking().AsSplitQuery().Include(c => c.User).Include(c => c.Package).ToListAsync();
        }
        public async Task<Customer?> GetACustomerByIdAsync(string id)
        {
            return await _context.Customers.Include(c => c.User).Include(c => c.Package).FirstOrDefaultAsync(m => m.Id == id); ;
        }
    }
}
