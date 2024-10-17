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
    public class ServiceBookingRepository : GenericRepository<ServiceBooking>
    {
        public ServiceBookingRepository() { }

        public ServiceBookingRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;
        public async Task<List<ServiceBooking>> GetServiceBookingsAsync()
        {
            return await _context.ServiceBookings.AsNoTracking().AsSplitQuery()
                .Include(sb => sb.Customer)
                .Include(sb => sb.Customer.User)
                .Include(sb => sb.Customer.Package)
                .ToListAsync();
        }
        public async Task<ServiceBooking?> GetAServiceBookingByIdAsync(string id)
        {
            return await _context.ServiceBookings
                .Include(sb => sb.Customer)
                .Include(sb => sb.Customer.User)
                .Include(sb => sb.Customer.Package)
                .FirstOrDefaultAsync(sb => sb.Id == id); ;
        }
        public async Task<List<ServiceBooking>> GetBookingsByCustomerIdAsync(string customerId)
        {
            return await _context.ServiceBookings.AsNoTracking().AsSplitQuery().Where(sb => sb.CustomerId == customerId).ToListAsync();
        }
    }
}
