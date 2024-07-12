using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Models;
using NDAccountManager.Core.Services;

namespace NDAccountManager.API.Controllers
{
    public class AccountsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Account> _service;

        public AccountsController(IMapper mapper, IService<Account> service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _service.GetAllAsync();
            var accountsDtos = _mapper.Map<List<AccountDto>>(accounts.ToList()); 
            return CreateActionResult(CustomResponseDto<List<AccountDto>>.Success(200, accountsDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _service.GetByIdAsync(id);
            var accountDto = _mapper.Map<AccountDto>(account);
            return CreateActionResult(CustomResponseDto<AccountDto>.Success(200,accountDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(AccountDto accountDto)
        {
            var account = await _service.AddAsync(_mapper.Map<Account>(accountDto));
            var accountsDto = _mapper.Map<AccountDto>(account);
            return CreateActionResult(CustomResponseDto<AccountDto>.Success(201, accountsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(AccountDto accountDto)
        {
            await _service.UpdateAsync(_mapper.Map<Account>(accountDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var account = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(account);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}