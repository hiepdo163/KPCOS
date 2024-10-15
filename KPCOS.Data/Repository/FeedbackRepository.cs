using KPCOS.Data.Base;
using KPCOS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Data.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback>
    {
        public FeedbackRepository() { }

        public FeedbackRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;
        public async Task<List<Feedback>> GetFeedbackInclude(Expression<Func<Feedback, bool>> condition)
        {
            return await _context.Feedbacks
                                 .Include(x => x.Project)
                                 .Include(x => x.Customer)
                                 .Where(condition)
                                 .ToListAsync();
        }

    }
}
