using AutoMapper;
using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Models;
using NDAccountManager.Core.Repositories;
using NDAccountManager.Core.Services;
using NDAccountManager.Core.UnitOfWorks;

namespace NDAccountManager.Service.Services
{
    public class SharedAccountService : Service<SharedAccount>, ISharedAccountService
    {
        private readonly ISharedAccountRepository _sharedAccountRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SharedAccountService(
            ISharedAccountRepository sharedAccountRepository,
            IAccountRepository accountRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(sharedAccountRepository, unitOfWork)
        {
            _sharedAccountRepository = sharedAccountRepository;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<SharedAccountDto>>> GetSharedAccountsByUserIdAsync(int userId)
        {
            var sharedAccounts = await _sharedAccountRepository.GetSharedAccountsByUserIdAsync(userId);
            var sharedAccountDtos = _mapper.Map<List<SharedAccountDto>>(sharedAccounts);
            return CustomResponseDto<List<SharedAccountDto>>.Success(200, sharedAccountDtos);
        }

        public async Task<CustomResponseDto<List<SharedAccountDto>>> GetSharedAccountsByAccountIdAsync(int accountId)
        {
            var sharedAccounts = await _sharedAccountRepository.GetSharedAccountsByAccountIdAsync(accountId);
            var sharedAccountDtos = _mapper.Map<List<SharedAccountDto>>(sharedAccounts);
            return CustomResponseDto<List<SharedAccountDto>>.Success(200, sharedAccountDtos);
        }

        public async Task<CustomResponseDto<SharedAccountDto>> AddSharedAccountAsync(SharedAccountDto sharedAccountDto)
        {
            var account = await _accountRepository.GetByIdAsync(sharedAccountDto.AccountId);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {sharedAccountDto.AccountId} not found.");
            }

            var user = await _userRepository.GetByIdAsync(sharedAccountDto.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {sharedAccountDto.UserId} not found.");
            }

            // Ayni kullaniciya ayni hesabin  halihazirda paylasilmis olup olmadigini kontrol
            var existingSharedAccount = await _sharedAccountRepository
                .GetSharedAccountsByUserIdAsync(sharedAccountDto.UserId);
            if (existingSharedAccount.Any(sa => sa.AccountId == sharedAccountDto.AccountId))
            {
                return CustomResponseDto<SharedAccountDto>.Fail(400, $"This account is already shared with the {user.Username}.");
            }

            if (!sharedAccountDto.IsUnlimited && !sharedAccountDto.ExpirationDate.HasValue)
            {
                throw new ArgumentException("ExpirationDate is required when IsUnlimited is false.");
            }

            var sharedAccount = _mapper.Map<SharedAccount>(sharedAccountDto);

            await _sharedAccountRepository.AddAsync(sharedAccount);
            await _unitOfWork.CommitAsync();

            var resultDto = _mapper.Map<SharedAccountDto>(sharedAccount);
            return CustomResponseDto<SharedAccountDto>.Success(201, resultDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteSharedAccountAsync(int userId, int accountId)
        {
            var sharedAccount = await _sharedAccountRepository.GetByIdAsync(userId, accountId);
            if (sharedAccount == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, $"SharedAccount with UserId {userId} and AccountId {accountId} not found.");
            }

            _sharedAccountRepository.Remove(sharedAccount);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}

#region AddSharedAccount0
//public async Task<CustomResponseDto<SharedAccountDto>> AddSharedAccountAsync(SharedAccountDto sharedAccountDto)
//{var account = await _accountRepository.GetByIdAsync(sharedAccountDto.AccountId);
//if (account == null)
//{
//    throw new KeyNotFoundException($"Account with ID {sharedAccountDto.AccountId} not found.");
//}

//var user = await _userRepository.GetByIdAsync(sharedAccountDto.UserId);
//if (user == null)
//{
//    throw new KeyNotFoundException($"User with ID {sharedAccountDto.UserId} not found.");
//}

//if (!sharedAccountDto.IsUnlimited && !sharedAccountDto.ExpirationDate.HasValue)
//{
//    throw new ArgumentException("ExpirationDate is required when IsUnlimited is false.");
//}

//var sharedAccountDto = new SharedAccountDto
//{
//    UserId = sharedAccountDto.UserId,
//    AccountId = sharedAccountDto.AccountId,
//    IsUnlimited = sharedAccountDto.IsUnlimited,
//    ExpirationDate = sharedAccountDto.ExpirationDate
//};

//await _sharedAccountRepository.AddAsync(sharedAccountDto);
//await _unitOfWork.CommitAsync();
//return sharedAccount;}
#endregion

#region DeleteSharedAccount0
//public async Task<CustomResponseDto<NoContent>> DeleteSharedAccountAsync(int userId, int accountId)
//{
//    var sharedAccount = await _sharedAccountRepository.GetByIdAsync(userId, accountId);
//    if (sharedAccount == null)
//    {
//        return CustomResponseDto<NoContent>.Fail(404, $"SharedAccount with UserId {userId} and AccountId {accountId} not found.");
//    }

//    _sharedAccountRepository.Remove(sharedAccount);
//    await _unitOfWork.CommitAsync();
//    return CustomResponseDto<NoContent>.Success(204);
//}
#endregion