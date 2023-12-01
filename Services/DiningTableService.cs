using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Services
{
    public class DiningTableService
    {
        private readonly DiningTableRepository _diningTableRepository;
        private readonly IMapper _mapper;

        public DiningTableService(DiningTableRepository diningTableRepository, IMapper mapper)
        {
            this._diningTableRepository = diningTableRepository;
            this._mapper = mapper;
        }

        public async Task<List<ResultDiningTableDto>> GetAllDiningTables()
        {
            List<ResultDiningTableDto> diningTableResults = await this._diningTableRepository.GetAllDiningTables();
            return diningTableResults;
        }

        public async Task CreateDiningTable(CreateDiningTableDto dataInvo)
        {
            DiningTable diningTableCreate = new DiningTable();
            // create data
            diningTableCreate.TableNumber = dataInvo.TableNumber;
            diningTableCreate.Capacity = dataInvo.Capacity;
            diningTableCreate.CreatedBy = dataInvo.CreatedBy;
            // save data
            await _diningTableRepository.SaveDiningTable(diningTableCreate);
        }
    }
}