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
    public class ProjectRepository : GenericRepository<Project>
    {
        public ProjectRepository() { }

        public ProjectRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;
        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            var projects = await _context.Projects
                .Include(p => p.Customer).ThenInclude(c => c.User)
                .Include(p => p.Designer).ThenInclude(d => d.User) 
                .Include(p => p.ConstructionStaff).ThenInclude(cs => cs.User) 
                .ToListAsync();
            return projects;
        }
        public async Task<Project> GetProjectByIdAsync(String id)
        {
            var project = await _context.Projects
                .Include(p => p.Customer).ThenInclude(c => c.User) 
                .Include(p => p.Designer).ThenInclude(d => d.User) 
                .Include(p => p.ConstructionStaff).ThenInclude(cs => cs.User) 
                .FirstOrDefaultAsync(p => p.Id == id);

            return project;
        }
        public async Task<IEnumerable<Project>> GetProjectsByFilterAsync(string? customerName, string? designerId, DateTime? startDate, string? status)
        {
            var query = _context.Projects
                .Include(p => p.Customer).ThenInclude(c => c.User)
                .Include(p => p.Designer).ThenInclude(d => d.User)
                .Include(p => p.ConstructionStaff).ThenInclude(cs => cs.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(p => p.Customer.User.Fullname.Contains(customerName));
            }

            if (!string.IsNullOrEmpty(designerId))
            {
                query = query.Where(p => p.Designer.Id == designerId);
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.StartDate.HasValue && p.StartDate.Value.Date >= startDate.Value.Date);
            }


            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status == status);
            }

            return await query.ToListAsync();
        }
    }
}
