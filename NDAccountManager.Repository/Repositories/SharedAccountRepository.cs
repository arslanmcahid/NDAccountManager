using Microsoft.EntityFrameworkCore;
using NDAccountManager.Core.Models;
using NDAccountManager.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDAccountManager.Repository.Repositories
{
    public class SharedAccountRepository : GenericRepository<SharedAccount>, ISharedAccountRepository
    {
        public SharedAccountRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<SharedAccount>> GetSharedAccountsByUserIdAsync(int userId)
        {
            return await _context.Set<SharedAccount>()
                .Include(x => x.Account)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<SharedAccount>> GetSharedAccountsByAccountIdAsync(int accountId)
        {
            return await _context.Set<SharedAccount>()
                .Include(x => x.User)
                .Where(x => x.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<SharedAccount> GetByIdAsync(int userId, int accountId)
        {
            return await _context.Set<SharedAccount>()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.AccountId == accountId);
        }
    }
}
