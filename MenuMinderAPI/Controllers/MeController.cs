using BusinessObjects.DTO.AccountDTO;
using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO;
using BusinessObjects.Enum;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Exceptions;

namespace MenuMinderAPI.Controllers
{

    [ApiController]
    [Route("/api/me")]

    public class MeController : Controller
    {
        private readonly MeService _meService;

        public MeController(MeService meService)
        {
            this._meService = meService;
        }

        // PUT: api/me/update-password
        [HttpPut("update-password")]
        public async Task<ActionResult> CreateAccount([FromBody] UpdatePasswordDto dataInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            ApiResponse<ResultAccountDTO> response = new ApiResponse<ResultAccountDTO>();
            await this._meService.updatePassword(userFromToken.AccountId, dataInvo);
            response.message = "Password has been updated.";

            return Ok(response);
        }
    }
}
