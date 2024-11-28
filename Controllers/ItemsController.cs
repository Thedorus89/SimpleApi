using Microsoft.AspNetCore.Mvc;
using SimpleApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<Item> Items = new()
        {
            new Item { Id = 1, Name = "Item1", Description = "Description1" },
            new Item { Id = 2, Name = "Item2", Description = "Description2" }
        };

        // GET: api/items
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return Ok(Items);
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public ActionResult<Item> CreateItem([FromBody] Item newItem)
        {
            newItem.Id = Items.Max(i => i.Id) + 1; // Auto-increment ID
            Items.Add(newItem);
            return CreatedAtAction(nameof(GetItem), new { id = newItem.Id }, newItem);
        }

        // PUT: api/items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(int id, [FromBody] Item updatedItem)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updatedItem.Name;
            existingItem.Description = updatedItem.Description;
            return NoContent();
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            Items.Remove(item);
            return NoContent();
        }
    }
}
