using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class FoodOrderRepository : IFoodOrderRepository
    {

        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FoodOrderRepository> _logger;

        public FoodOrderRepository(Menu_minder_dbContext context, IMapper mapper, ILogger<FoodOrderRepository> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task InsertBulk(List<FoodOrder> datas)
        {
            try
            {
                datas.ForEach(data =>
                {
                    this._context.FoodOrders.Add(data);
                });
                await this._context.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
