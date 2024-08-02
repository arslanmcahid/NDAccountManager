using NDAccountManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Core.Repositories
{
    public interface ISharedAccountRepository : IGenericRepository<SharedAccount>
    {
        Task<List<SharedAccount>> GetSharedAccountsByUserIdAsync(int userId);
        Task<List<SharedAccount>> GetSharedAccountsByAccountIdAsync(int accountId);
        Task<SharedAccount> GetByIdAsync(int userId, int accountId);

    }
}
