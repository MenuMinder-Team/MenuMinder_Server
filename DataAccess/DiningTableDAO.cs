using BusinessObjects.DataAccess;
using System.Runtime.CompilerServices;

namespace DataAccess
{
    public class DiningTableDAO
    {
        private static readonly menu_minder_dbContext context = new menu_minder_dbContext();

        public static List<DiningTable> GetAllDiningTables()
        {
            var listTable = new List<DiningTable>();
            try
            {
                listTable = context.DiningTables.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listTable;
        }

        public static DiningTable FindDiningTableById(int tableId)
        {
            DiningTable table = new DiningTable();
            try
            {
                table = context.DiningTables.SingleOrDefault(d => d.TableId == tableId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return table;
        }

        public static void SaveDiningTable(DiningTable table)
        {
            try {
                context.DiningTables.Add(table);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateDiningTable(DiningTable table)
        {
            try
            {
                context.Entry<DiningTable>(table).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteDiningTable(DiningTable table)
        {
            try
            {
                var p1 = context.DiningTables.SingleOrDefault(
                    c => c.TableId == table.TableId);
                context.DiningTables.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}