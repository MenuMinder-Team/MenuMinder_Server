using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.ServingDTO;
using BusinessObjects.Enum;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Services.Exceptions;

namespace Services
{
    public class ServingService
    {
        private readonly IServingRepository _servingRepository;
        private readonly IDiningTableRepository _diningTableRepository;
        private readonly ITableUsedRepository _tableUsedRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServingService> _logger;


        public ServingService(IMapper mapper, ILogger<ServingService> logger, IServingRepository servingRepository, IDiningTableRepository diningTableRepository, ITableUsedRepository tableUsedRepository)
        {
            this._servingRepository = servingRepository;
            this._diningTableRepository = diningTableRepository;
            this._tableUsedRepository = tableUsedRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<Serving> createServing(CreateServingDTO dataInvo)
        {
            try
            {
                Serving servingCreate = new Serving();
                servingCreate.CreatedBy = dataInvo.CreatedBy;
                servingCreate.NumberOfCutomer = dataInvo.NumberOfCutomer;
                servingCreate.TimeIn = DateTime.Parse(dataInvo.TimeIn.ToString());
                servingCreate.TimeOut = null;

                // Check table availble
                List<int> idTableInvos = dataInvo.DiningTableIds;
                List<DiningTable> tableExists = await this._diningTableRepository.GetListTableWithIds(idTableInvos);

                // Check table not exist
                var idTableExists = tableExists.Where(tb => tb.Status == EnumTableStatus.AVAILABLE.ToString()).Select(tb => tb.TableId).ToList();
                foreach (var id in idTableInvos)
                {
                    if (!idTableExists.Contains(id))
                    {
                        throw new BadRequestException($"DiningTable with ID = {id} is not Availble");
                    }
                }

                // create Serving
                Serving servingResult = await this._servingRepository.InsertServing(servingCreate);
                // create bulk TableUsed
                List<TableUsed> tableUsedCreate = idTableInvos.Select(tableId => new TableUsed { ServingId = servingResult.ServingId, TableId = tableId }).ToList();
                await this._tableUsedRepository.InsertBulk(tableUsedCreate);
                // update DiningTable status
                await this._diningTableRepository.UpdateBulkAvailbleByIds(idTableInvos);

                return servingResult;
            }
            catch (BadRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
