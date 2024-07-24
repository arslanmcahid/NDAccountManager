using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Core.Repositories
{
    public interface IAccountRepository :IGenericRepository<Account>
    {
        Task<List<Account>> FilteredAccountsForPlatform(); // Listing process from A to Z by platform
        Task<List<Account>> GetAccountsWithUsernameAndPassword(); //Hesaplari sadece username ve password degerleri ile goster --> bunun icin ozellestirilmis DTO
        Task<List<Account>> AccountsThatPlatformNameIncluded(string value);
        Task<List<Account>> FilteredAccountsForUsername();
        Task<List<Account>> AccountsThatUsernameIncluded(string value);
    }
}
