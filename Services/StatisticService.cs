using BusinessObjects.DataModels;
using BusinessObjects.DTO.StatisticDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StatisticService
    {
        private readonly Menu_minder_dbContext _context;
        private readonly ILogger<StatisticService> _logger;

        public StatisticService(Menu_minder_dbContext context, ILogger<StatisticService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ResultRevenueDTO>> GetRevenueReport(DateTime? fromDate, DateTime? toDate, string reportType)
        {
            DateTime now = DateTime.Now;
            return await _context.GetRevenueReport(fromDate.GetValueOrDefault(now.AddDays(-30)), toDate.GetValueOrDefault(now), reportType);
        }
    }
}
