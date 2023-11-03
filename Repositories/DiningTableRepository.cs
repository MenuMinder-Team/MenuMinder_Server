using BusinessObjects.DataAccess;
using DataAccess;

namespace Repositories
{
    public class DiningTableRepository
    {
        public void DeleteDiningTable(DiningTable p) => DiningTableDAO.DeleteDiningTable(p);
        public void SaveDiningTable(DiningTable p) => DiningTableDAO.SaveDiningTable(p);
        public void UpdateDiningTable(DiningTable p) => DiningTableDAO.UpdateDiningTable(p);
        public List<DiningTable> GetAllDiningTables() => DiningTableDAO.GetAllDiningTables();
        public DiningTable GetDiningTableById(int id) => DiningTableDAO.FindDiningTableById(id);
    }
}