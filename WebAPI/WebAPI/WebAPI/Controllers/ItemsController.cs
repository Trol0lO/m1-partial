using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly ItemsDB ItemDB;
        public ItemsController(ItemsDB ItemDb)
        {
            this.ItemDB = ItemDb;
        }
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await ItemDB.Items.ToListAsync();
            return Ok(items);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItems([FromBody]Items itm)
        {
            itm.Id = new Guid();

            await ItemDB.Items.AddAsync(itm);
            await ItemDB.SaveChangesAsync();
            return Ok(itm);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateItems([FromRoute] Guid id,[FromBody] Items itm)
        {
           var item = await ItemDB.Items.FirstOrDefaultAsync(a => a.Id == id);

            if (item != null)
            {
                item.Name = itm.Name;
                item.Code = itm.Code;
                item.Brand = itm.Brand;
                item.UnitPrice = itm.UnitPrice; 
                await ItemDB.SaveChangesAsync();

                return Ok(itm);

            }
            else
            {
                return NotFound("Item not found");
            }
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteItems([FromRoute] Guid id)
        {
            var item = await ItemDB.Items.FirstOrDefaultAsync(a => a.Id == id);

            if (item != null)
            {
                ItemDB.Items.Remove(item);
                await ItemDB.SaveChangesAsync();

                return Ok(item);

            }
            else
            {
                return NotFound("Item not found");
            }

        }
    }
}
