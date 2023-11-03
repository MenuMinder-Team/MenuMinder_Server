using BusinessObjects.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiningTableController : ControllerBase
    {
        private DiningTableRepository repository = new DiningTableRepository();

        //GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<DiningTable>> GetAllDiningTables() => repository.GetAllDiningTables();

        //POST: ProductsController/Products
        [HttpPost]
        public IActionResult PostProduct(DiningTable table)
        {
            repository.SaveDiningTable(table);
            return Ok(table);
        }

        //GET: ProductsController/Delete/5
        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetDiningTableById(id);
            if (p == null)
                return NotFound();
            repository.DeleteDiningTable(p);
            return Ok(p);
        }

        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, DiningTable table)
        {
            if (id != table.TableId)
            {
                return BadRequest();
            }
            var pTmp = repository.GetDiningTableById(id);
            if (pTmp == null)
                return NotFound();
            repository.UpdateDiningTable(table);
            return Ok(table);
        }
    }
}