using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.FoodOrderDTO;
using BusinessObjects.Enum;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Services
{
    public class FoodOrderService
    {
        private readonly IFoodOrderRepository _foodOrderRepository;
        private readonly ILogger<FoodOrderService> _logger;

        public FoodOrderService(ILogger<FoodOrderService> _logger, IFoodOrderRepository _foodOrderRepository)
        {
            this._logger = _logger;
            this._foodOrderRepository = _foodOrderRepository;
        }

        public async Task<List<FoodOrderShortDto>> GetFoodOrder(string status)
        {
            try
            {
                // check food order status
                string statusFoodOrder = "";
                switch (status)
                {
                    case "PENDING":
                        statusFoodOrder = status;
                        break;
                    case "PROCESSING":
                        statusFoodOrder = status;
                        break;
                    case "SERVED":
                        statusFoodOrder = status;
                        break;
                    default:
                        statusFoodOrder = "ALL";
                        break;
                }

                // get list food orders
                List<FoodOrderShortDto> foodOrders = await this._foodOrderRepository.FindFoodOrderWithStatus(statusFoodOrder);
                return foodOrders;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
