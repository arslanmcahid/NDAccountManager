using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Models;

namespace NDAccountManager.Core.Services
{
    public interface ISharedAccountService : IService<SharedAccount>
    {
        Task<CustomResponseDto<List<SharedAccountDto>>> GetSharedAccountsByUserIdAsync(int userId);
        Task<CustomResponseDto<List<SharedAccountDto>>> GetSharedAccountsByAccountIdAsync(int accountId);
        Task<CustomResponseDto<SharedAccountDto>> AddSharedAccountAsync(SharedAccountDto sharedAccountDto);
        Task<CustomResponseDto<NoContentDto>> DeleteSharedAccountAsync(int userId, int accountId);
    }
}
