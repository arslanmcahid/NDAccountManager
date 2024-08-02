using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Models;
using NDAccountManager.Core.Services;

namespace NDAccountManager.API.Controllers
{
    public class SharedAccountController : CustomBaseController
    {
        private readonly ISharedAccountService _sharedAccountService;

        public SharedAccountController(ISharedAccountService sharedAccountService)
        {
            _sharedAccountService = sharedAccountService;
        }

        // GET: api/sharedaccount/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetSharedAccountsByUserId(int userId)
        {
            var sharedAccounts = await _sharedAccountService.GetSharedAccountsByUserIdAsync(userId);
            return Ok(sharedAccounts);
        }

        // GET: api/sharedaccount/account/{accountId}
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetSharedAccountsByAccountId(int accountId)
        {
            var sharedAccounts = await _sharedAccountService.GetSharedAccountsByAccountIdAsync(accountId);
            return Ok(sharedAccounts);
        }

        // POST: api/sharedaccount
        [HttpPost]
        public async Task<IActionResult> AddSharedAccount([FromBody] SharedAccountDto sharedAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _sharedAccountService.AddSharedAccountAsync(sharedAccountDto);
                if (!response.IsSuccess)
                {
                    return BadRequest(response.Errors);
                }

                var createdSharedAccount = response.Data; // response un datasindan gelen sharedaccountDTO
                return CreatedAtAction(nameof(GetSharedAccountsByAccountId), new { accountId = createdSharedAccount.AccountId }, createdSharedAccount);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/sharedaccount/{userId}/{accountId}
        [HttpDelete("{userId}/{accountId}")]
        public async Task<IActionResult> DeleteSharedAccount(int userId, int accountId)
        {
            try
            {
                await _sharedAccountService.DeleteSharedAccountAsync(userId, accountId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}