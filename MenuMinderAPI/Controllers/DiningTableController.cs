using System.Collections.Generic;
using System.Net;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/dining-tables")]
    public class DiningTableController : ControllerBase
    {
        private readonly DiningTableService _diningTableService;
        private readonly ILogger<DiningTableRepository> _logger;

        public DiningTableController(DiningTableService diningTableService, ILogger<DiningTableRepository> logger)
        {
            this._diningTableService = diningTableService;
            this._logger = logger;
        }

        // GET: api/dining-tables
        [HttpGet]
        public async Task<ActionResult> GetAllDiningTables()
        {
            ApiResponse<List<ResultDiningTableDto>> response = new ApiResponse<List<ResultDiningTableDto>>();

            try
            {
                List <ResultDiningTableDto> results = await this._diningTableService.GetAllDiningTables();
                response.data = results;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // POST: api/dining-tables/create
        [HttpPost("create")]
        public async Task<ActionResult> CreateDiningTable([FromBody] CreateDiningTableDto dataInvo)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            try
            {
                await this._diningTableService.CreateDiningTable(dataInvo);
                response.message = "created dining table success.";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }
    }
}