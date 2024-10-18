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

    }
}
