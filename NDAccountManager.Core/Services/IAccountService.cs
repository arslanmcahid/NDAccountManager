using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Models;

namespace NDAccountManager.Core.Services
{
    public interface IAccountService : IService<Account>
    {
        Task<CustomResponseDto<List<AccountDto>>> FilteredAccountsForPlatform(); //Platforma gore filtreleme islemi
        Task<CustomResponseDto<List<NameAndPasswordAccountDto>>> GetAccountsWithUsernameAndPassword(); //Hesaplari sadece username ve password degerleri ile goster --> bunun icin ozellestirilmis DTO
        Task<CustomResponseDto<List<AccountDto>>> AccountsThatPlatformNameIncluded(string value);
        Task<CustomResponseDto<List<AccountDto>>> FilteredAccountsForUsername();
        Task<CustomResponseDto<List<AccountDto>>> AccountsThatUsernameIncluded(string value);
    }
}
