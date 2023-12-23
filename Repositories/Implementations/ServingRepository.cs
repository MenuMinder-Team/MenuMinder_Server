using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class ServingRepository : IServingRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly ILogger<ServingRepository> _logger;

        public ServingRepository(Menu_minder_dbContext context, ILogger<ServingRepository> _logger)
        {
            this._context = context;
            this._logger = _logger;
        }

        public async Task<Serving> InsertServing(Serving data)
        {
            try
            {
                this._context.Servings.Add(data);
                await this._context.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
