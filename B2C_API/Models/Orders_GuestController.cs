using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace B2C_API.Models
{
    public class Orders_GuestController : ApiController
    {
        private PhoneManagerEntities db = new PhoneManagerEntities();

        // GET: api/Orders_Guest
        public IQueryable<Orders_Guest> GetOrders_Guest()
        {
            return db.Orders_Guest;
        }

        // GET: api/Orders_Guest/5
        [ResponseType(typeof(Orders_Guest))]
        public IHttpActionResult GetOrders_Guest(string id)
        {
            Orders_Guest orders_Guest = db.Orders_Guest.Find(id);
            if (orders_Guest == null)
            {
                return NotFound();
            }

            return Ok(orders_Guest);
        }

        // PUT: api/Orders_Guest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrders_Guest(string id, Orders_Guest orders_Guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orders_Guest.OrderID)
            {
                return BadRequest();
            }

            db.Entry(orders_Guest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Orders_GuestExists(id))
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

        // POST: api/Orders_Guest
        [ResponseType(typeof(Orders_Guest))]
        public IHttpActionResult PostOrders_Guest(Orders_Guest orders_Guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders_Guest.Add(orders_Guest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Orders_GuestExists(orders_Guest.OrderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orders_Guest.OrderID }, orders_Guest);
        }

        // DELETE: api/Orders_Guest/5
        [ResponseType(typeof(Orders_Guest))]
        public IHttpActionResult DeleteOrders_Guest(string id)
        {
            Orders_Guest orders_Guest = db.Orders_Guest.Find(id);
            if (orders_Guest == null)
            {
                return NotFound();
            }

            db.Orders_Guest.Remove(orders_Guest);
            db.SaveChanges();

            return Ok(orders_Guest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Orders_GuestExists(string id)
        {
            return db.Orders_Guest.Count(e => e.OrderID == id) > 0;
        }
    }
}