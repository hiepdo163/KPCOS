using KPCOS.Data.Base;
using KPCOS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Data.Repository
{
    public class ConsultationRepository : GenericRepository<Consultation>
    {
        public ConsultationRepository() { }

        public ConsultationRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;
    }
}
