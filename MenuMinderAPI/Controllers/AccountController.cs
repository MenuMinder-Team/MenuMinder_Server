using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using BusinessObjects.DTO.AccountDTO;
using MenuMinderAPI.MiddleWares;
using System.Security.Claims;
using BusinessObjects.Enum;
using Services.Exceptions;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController (AccountService accountService)
        {
            this._accountService = accountService;
        }

        // POST: api/accounts/create
        [HttpPost("create")]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountDto dataInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            // Check ROLE
            if(userFromToken.Role != EnumRole.ADMIN.ToString())
            {
                throw new UnauthorizedException("Only ADMIN have permission to access this resource.");
            }

            ApiResponse<ResultAccountDTO> response = new ApiResponse<ResultAccountDTO>();
            ResultAccountDTO resultAccount = await this._accountService.createAccount(dataInvo);
            response.data = resultAccount;
            response.message = "create account success.";

            return Ok(response);
        }
    }
}
