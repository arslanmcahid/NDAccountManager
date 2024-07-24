using AutoMapper;
using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Models;
using NDAccountManager.Core.Repositories;
using NDAccountManager.Core.Services;
using NDAccountManager.Core.UnitOfWorks;

namespace NDAccountManager.Service.Services
{
    public class AccountService : Service<Account>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountService(IGenericRepository<Account> repository, IUnitOfWork unitOfWork, IMapper mapper, IAccountRepository accountRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<CustomResponseDto<List<AccountDto>>> AccountsThatPlatformNameIncluded(string value)
        {
            var accounts = await _accountRepository.AccountsThatPlatformNameIncluded(value);
            var accountsDto = _mapper.Map<List<AccountDto>>(accounts);
            return CustomResponseDto<List<AccountDto>>.Success(200, accountsDto);
        }

        public async Task<CustomResponseDto<List<AccountDto>>> AccountsThatUsernameIncluded(string value)
        {
            var accounts = await _accountRepository.AccountsThatUsernameIncluded(value);
            var accountsDto = _mapper.Map<List<AccountDto>>(accounts);
            return CustomResponseDto<List<AccountDto>>.Success(200, accountsDto);
        }

        public async Task<CustomResponseDto<List<AccountDto>>> FilteredAccountsForPlatform()
        {
            var accounts = await _accountRepository.FilteredAccountsForPlatform();
            var accountsDto = _mapper.Map<List<AccountDto>>(accounts);
            return CustomResponseDto<List<AccountDto>>.Success(200,accountsDto);
        }

        public async Task<CustomResponseDto<List<AccountDto>>> FilteredAccountsForUsername()
        {
            var accounts = await _accountRepository.FilteredAccountsForUsername();
            var accountsDto = _mapper.Map<List<AccountDto>>(accounts);
            return CustomResponseDto<List<AccountDto>>.Success(200, accountsDto);
        }

        public async Task<CustomResponseDto<List<NameAndPasswordAccountDto>>> GetAccountsWithUsernameAndPassword()
        {
            var accounts = await _accountRepository.GetAccountsWithUsernameAndPassword();
            var accountsDto = _mapper.Map<List<NameAndPasswordAccountDto>>(accounts);
            return CustomResponseDto<List<NameAndPasswordAccountDto>>.Success(200, accountsDto);
        }
    }
}
