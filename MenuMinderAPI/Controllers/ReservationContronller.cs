using System.Security.Claims;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services;
using BusinessObjects.DTO.ReservationDTO;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/reservations")]
    public class ReservationContronller : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationContronller(ReservationService reservationService)
        {
            this._reservationService = reservationService;
        }

        // POST: api/reservations/create
        [HttpPost("create")]
        public async Task<ActionResult> CreateReservation(CreateReservationDto dataInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            dataInvo.CreatedBy = Guid.Parse(userFromToken.AccountId);

            ApiResponse<NoContentResult> response = new ApiResponse<NoContentResult>();
            await this._reservationService.createReservation(dataInvo);
            response.message = "Created reservation success";

            return Ok(response);
        }
    }
}
