using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;

namespace Repositories.Interfaces
{
    public interface IDiningTableRepository
    {
        public Task<List<ResultDiningTableDto>> GetAllDiningTables();
        public Task SaveDiningTable(DiningTable table);

        /*public DiningTable FindDiningTableById(int tableId);
        public void UpdateDiningTable(DiningTable table);
        public void DeleteDiningTable(DiningTable table);*/

    }
}
