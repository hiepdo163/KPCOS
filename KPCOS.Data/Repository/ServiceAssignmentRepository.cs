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
    public class ServiceAssignmentRepository : GenericRepository<ServiceAssignment>
    {
        public ServiceAssignmentRepository() { }

        public ServiceAssignmentRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;
        public async Task<PagedResultResponse<ServiceAssignment>> GetServiceAssignmentsAsync(QueryPagedServiceAssignment query)
        {
            var serviceAssignments = _context.ServiceAssignments.OrderByDescending(sa => sa.AssignDate).AsNoTracking().AsSplitQuery();

            if (!string.IsNullOrEmpty(query.ServiceBookingId))
            {
                serviceAssignments = serviceAssignments.Where(e => e.ServiceBookingId == query.ServiceBookingId);
            }
            if (!string.IsNullOrEmpty(query.EmployeeId))
            {
                serviceAssignments = serviceAssignments.Where(e => e.EmployeeId == query.EmployeeId);
            }
            if (!string.IsNullOrEmpty(query.Status))
            {
                serviceAssignments = serviceAssignments.Where(e => e.Status == query.Status);
            }

                        
            var data = await serviceAssignments.Skip((query.PageNumber - 1) * 10).Take(10)
                .Include(sa => sa.ServiceBooking)
                .Include(sa => sa.ServiceBooking.Customer)
                .Include(sa => sa.ServiceBooking.Customer.User)
                .Include(sa => sa.Employee)
                .Include(sa => sa.Employee.Supervisor)
                .Include(sa => sa.Employee.Supervisor.User)
                .Include(sa => sa.Employee.User)
                .ToListAsync();
            return new PagedResultResponse<ServiceAssignment>
            {
                TotalCount = await serviceAssignments.CountAsync(),
                PageNumber = query.PageNumber,
                Items = data
            };
        }
        public async Task<List<ServiceAssignment>> GetAllServiceAssignment()
        {
            return await _context.ServiceAssignments.AsNoTracking().AsSplitQuery()
                .Include(sa => sa.ServiceBooking)
                .Include(sa => sa.ServiceBooking.Customer)
                .Include(sa => sa.ServiceBooking.Customer.User)
                .Include(sa => sa.Employee)
                .Include(sa => sa.Employee.Supervisor)
                .Include(sa => sa.Employee.Supervisor.User)
                .Include(sa => sa.Employee.User)
                .ToListAsync();
        }
        public async Task<ServiceAssignment?> GetAServiceAssignmentByIdAsync(string id)
        {
            return await _context.ServiceAssignments
                .Include(sa => sa.ServiceBooking)
                .Include(sa => sa.ServiceBooking.Customer)
                .Include(sa => sa.ServiceBooking.Customer.User)
                .Include(sa => sa.Employee)
                .Include(sa => sa.Employee.Supervisor)
                .Include(sa => sa.Employee.Supervisor.User)
                .Include(sa => sa.Employee.User)
                .FirstOrDefaultAsync(m => m.Id == id); ;
        }
        public async Task<List<ServiceAssignment>> GetAssignmentsByEmployeeIdAsync(string employeeId)
        {
            return await _context.ServiceAssignments.AsNoTracking().AsSplitQuery().Where(sa => sa.EmployeeId == employeeId).ToListAsync();
        }
        public async Task<List<ServiceAssignment>> GetAssignmentsByServiceBookingIdAsync(string bookingId)
        {
            return await _context.ServiceAssignments.AsNoTracking().AsSplitQuery().Where(sa => sa.ServiceBookingId == bookingId).ToListAsync();
        }
    }
}
