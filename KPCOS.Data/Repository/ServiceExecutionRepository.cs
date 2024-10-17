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
    public class ServiceExecutionRepository : GenericRepository<ServiceExecution>
    {
        public ServiceExecutionRepository() { }

        public ServiceExecutionRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;
        public async Task<List<ServiceExecution>> GetServiceExecutionsAsync()
        {
            return await _context.ServiceExecutions.AsNoTracking().AsSplitQuery()
                .Include(se => se.Employee)
                .Include(se => se.Employee.User)
                .Include(se => se.Employee.Supervisor)
                .Include(se => se.Employee.Supervisor.User)
                .Include(se => se.ServiceBooking)
                .Include(se => se.ServiceBooking.Customer)
                .Include(se => se.ServiceBooking.Customer.User)
                .ToListAsync();
        }
        public async Task<ServiceExecution?> GetAServiceExecutionByIdAsync(string id)
        {
            return await _context.ServiceExecutions
                .Include(se => se.Employee)
                .Include(se => se.Employee.User)
                .Include(se => se.Employee.Supervisor)
                .Include(se => se.Employee.Supervisor.User)
                .Include(se => se.ServiceBooking)
                .Include(se => se.ServiceBooking.Customer)
                .Include(se => se.ServiceBooking.Customer.User)
                .FirstOrDefaultAsync(se => se.Id == id); ;
        }
        public async Task<List<ServiceExecution>> GetExecutionsByEmployeeIdAsync(string employeeId)
        {
            return await _context.ServiceExecutions.AsNoTracking().AsSplitQuery().Where(se => se.EmployeeId == employeeId).ToListAsync();
        }
        public async Task<List<ServiceExecution>> GetExecutionsByServiceBookingIdAsync(string bookingId)
        {
            return await _context.ServiceExecutions.AsNoTracking().AsSplitQuery().Where(se => se.ServiceBookingId == bookingId).ToListAsync();
        }
    }
}
