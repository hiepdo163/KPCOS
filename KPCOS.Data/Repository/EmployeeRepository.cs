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
    public class EmployeeRepository : GenericRepository<Employee>
    {
        public EmployeeRepository() { }

        public EmployeeRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.AsNoTracking().AsSplitQuery()
                .Include(e => e.Supervisor)
                .Include(e => e.Supervisor.User)
                .Include(e => e.User)
                .ToListAsync();
        }
        public async Task<Employee?> GetAnEmployeeByIdAsync(string id)
        {
            return await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Supervisor)
                .Include(e => e.Supervisor.User)
                .FirstOrDefaultAsync(e => e.Id == id); ;
        }
        public async Task<List<Employee>> GetEmployeesByUserIdAsync(string userId)
        {
            return await _context.Employees.AsNoTracking().AsSplitQuery().Where(e => e.UserId == userId).ToListAsync();
        }
        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            var employees = await _context.Employees
                .ToListAsync();
            return employees;
        }
    }
}
