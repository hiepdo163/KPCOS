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
    public class ServiceBookingRepository : GenericRepository<ServiceBooking>
    {
        public ServiceBookingRepository() { }

        public ServiceBookingRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;
        public async Task<List<ServiceBooking>> GetAllServiceBookings()
        {
            return await _context.ServiceBookings.AsNoTracking().AsSplitQuery()
                .Include(sb => sb.Customer)
                .Include(sb => sb.Customer.User)
                .Include(sb => sb.Customer.Package)
                .ToListAsync();
        }
        public async Task<PagedResultResponse<ServiceBooking>> GetServiceBookingsAsync(QueryPagedServiceBooking query)
        {
            var serviceBookings = _context.ServiceBookings.OrderByDescending(sb => sb.BookingDate).AsNoTracking().AsSplitQuery();
            if (!string.IsNullOrEmpty(query.CustomerId))
            {
                serviceBookings = serviceBookings.Where(e => e.CustomerId == query.CustomerId);
            }
            if (!string.IsNullOrEmpty(query.ServiceType))
            {
                serviceBookings = serviceBookings.Where(e => e.ServiceType.ToLower().Contains(query.ServiceType.ToLower()));
            }
            if (query.StartDate.HasValue)
            {
                serviceBookings = serviceBookings.Where(e => e.StartDate >= query.StartDate);
            }
            if (query.EndDate.HasValue)
            {
                serviceBookings = serviceBookings.Where(e => e.EndDate <= query.EndDate);
            }
            if (!string.IsNullOrEmpty(query.Status))
            {
                serviceBookings = serviceBookings.Where(e => e.Status == query.Status);
            }

            var data = await serviceBookings.Skip((query.PageNumber - 1) * 10).Take(10)
                .Include(sb => sb.Customer)
                .Include(sb => sb.Customer.User)
                .Include(sb => sb.Customer.Package)
                .ToListAsync();
            return new PagedResultResponse<ServiceBooking>
            {
                TotalCount = await serviceBookings.CountAsync(),
                PageNumber = query.PageNumber,
                Items = data
            };
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
