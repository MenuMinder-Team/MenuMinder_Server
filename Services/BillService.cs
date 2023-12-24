using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.BillDTO;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Services
{
    public class BillService
    {
        private readonly ILogger<BillService> _logger;
        private readonly IBillRepository _billRepository;
        private readonly IServingRepository _servingRepository;

        public BillService(ILogger<BillService> logger, IBillRepository billRepository, IServingRepository servingRepository)
        {
            this._logger = logger;
            this._billRepository = billRepository;
            this._servingRepository = servingRepository;
        }

        public async Task createBill(BillCreateDto dataInvo)
        {
            try
            {
                var totalPrice = await this._servingRepository.CalcTotalPrice(dataInvo.ServingId);
                Bill billCreate = new Bill { 
                    ServingId = dataInvo.ServingId,
                    TotalPrice = (int)totalPrice,
                    CreatedBy = (Guid)dataInvo.CreatedBy,
                };

                await this._billRepository.InsertBill(billCreate);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
