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
    }
}
