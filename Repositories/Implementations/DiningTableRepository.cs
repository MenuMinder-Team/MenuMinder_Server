/*using BusinessObjects.DataModels;*/
using System.Net.NetworkInformation;
using System;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Repositories
{
    public class DiningTableRepository : IDiningTableRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DiningTableRepository> _logger;

        public DiningTableRepository(Menu_minder_dbContext context, IMapper mapper, ILogger<DiningTableRepository> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<List<ResultDiningTableDto>> GetAllDiningTables()
        {

            List <ResultDiningTableDto> data  = new List<ResultDiningTableDto>();
            try
            { 
                data = await _context.DiningTables.Select(diningTable => _mapper.Map<ResultDiningTableDto>(diningTable)).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;
        }

        public async Task SaveDiningTable(DiningTable table)
        {
            try
            {
                this._context.DiningTables.Add(table);
                await this._context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw new Exception(e.Message);
            }
        }
    }
}
