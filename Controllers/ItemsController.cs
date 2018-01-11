using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using InvoiceApp.Models;
using InvoiceApp.DTOs;
using System.Linq.Expressions;

namespace InvoiceApp.Controllers
{
    public class ItemsController : ApiController
    {
        private InvoiceEntities1 db = new InvoiceEntities1();

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<Item, ItemDto>> AsItemDto =
            x => new ItemDto
            {
                ItemId = x.ItemId,
                Code = x.Code,
                Name = x.Name,
                Value = x.Value
            };

        // GET: api/Items
        public IQueryable<ItemDto> GetItems()
        {
            return db.Items.Select(AsItemDto);
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> GetItem(Guid id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItem(Guid id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.ItemId)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Items
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            item.ItemId = Guid.NewGuid();
            db.Items.Add(item);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemExists(item.ItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> DeleteItem(Guid id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            await db.SaveChangesAsync();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(Guid id)
        {
            return db.Items.Count(e => e.ItemId == id) > 0;
        }
    }
}