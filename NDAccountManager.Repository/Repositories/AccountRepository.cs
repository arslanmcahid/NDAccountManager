using Microsoft.EntityFrameworkCore;
using NDAccountManager.Core.Models;
using NDAccountManager.Core.Repositories;

namespace NDAccountManager.Repository.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<List<Account>> AccountsThatPlatformNameIncluded(string value)
        {
            return await _context.Accounts.Where(x => x.Platform.Contains(value)).ToListAsync();
        }

        public async Task<List<Account>> AccountsThatUsernameIncluded(string value)
        {
            return await _context.Accounts.Where(x=>x.Username.Contains(value)).ToListAsync();
        }


        public async Task<List<Account>> FilteredAccountsForPlatform()
        {
            var accounts = await _context.Accounts.AsQueryable().ToListAsync();

            var groupedAndOrderedAccounts = accounts.GroupBy(x => x.Platform)
                                            .OrderBy(x=>x.Key).SelectMany(x=>x).ToList();
            return groupedAndOrderedAccounts;

            #region
            // asenumerable() sonrasi bak
            /*var groupedAccounts = await _context.Accounts.GroupBy(x=>x.Platform)
                                                        .OrderBy(x=>x.Key)
                                                        .ToListAsync();
            var accounts = groupedAccounts.SelectMany(x=>x).ToList();
            return accounts;*/
            //var deneme1 = _context.Accounts.GroupBy(x => x.Platform).Where(group => group.Count() > 0).SelectMany(x => x).ToListAsync();
            //return await deneme1;
            #endregion
        }

        public async Task<List<Account>> FilteredAccountsForUsername()
        {
            var accounts = await _context.Accounts.AsQueryable().ToListAsync();
            var groupedAndOrderedAccounts = accounts.GroupBy(x=>x.Username)
                                                    .OrderBy(x=>x.Key)
                                                    .SelectMany(x=>x).ToList();
            return groupedAndOrderedAccounts;
        }

        public async Task<List<Account>> GetAccountsWithUsernameAndPassword()
        {
            var accounts = await _context.Accounts.Select(
                a => new Account
                {
                    Username = a.Username,
                    PasswordHash = a.PasswordHash,
                })
                .ToListAsync();
            return accounts;
        }
    }
}
