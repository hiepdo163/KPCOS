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
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository() { }

        public UserRepository(FA24_SE1717_PRN231_G4_KPCOSContext context) => _context = context;

        public async Task<List<User>> GetCustomersAsync()
        {
            return await _context.Users.AsNoTracking().AsSplitQuery().Include(c => c.Customers).ToListAsync();
        }
        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Users
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }
    }
}
