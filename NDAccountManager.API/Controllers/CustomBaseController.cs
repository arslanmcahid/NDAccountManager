using Microsoft.AspNetCore.Mvc;
using NDAccountManager.Core.DTOs;

namespace NDAccountManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        [NonAction]//Endpoint degil bu
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if(response.StatusCode == 204)//No Content
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            }
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

    }
}
