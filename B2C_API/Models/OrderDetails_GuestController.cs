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
    public class OrderDetails_GuestController : ApiController
    {
        private PhoneManagerEntities db = new PhoneManagerEntities();

        // GET: api/OrderDetails_Guest
        public IQueryable<OrderDetails_Guest> GetOrderDetails_Guest()
        {
            return db.OrderDetails_Guest;
        }

        // GET: api/OrderDetails_Guest/5
        [ResponseType(typeof(OrderDetails_Guest))]
        public IHttpActionResult GetOrderDetails_Guest(string id)
        {
            OrderDetails_Guest orderDetails_Guest = db.OrderDetails_Guest.Find(id);
            if (orderDetails_Guest == null)
            {
                return NotFound();
            }

            return Ok(orderDetails_Guest);
        }

        // PUT: api/OrderDetails_Guest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderDetails_Guest(string id, OrderDetails_Guest orderDetails_Guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderDetails_Guest.OrderID)
            {
                return BadRequest();
            }

            db.Entry(orderDetails_Guest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetails_GuestExists(id))
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

        // POST: api/OrderDetails_Guest
        [ResponseType(typeof(OrderDetails_Guest))]
        public IHttpActionResult PostOrderDetails_Guest(OrderDetails_Guest orderDetails_Guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails_Guest.Add(orderDetails_Guest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderDetails_GuestExists(orderDetails_Guest.OrderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderDetails_Guest.OrderID }, orderDetails_Guest);
        }

        // DELETE: api/OrderDetails_Guest/5
        [ResponseType(typeof(OrderDetails_Guest))]
        public IHttpActionResult DeleteOrderDetails_Guest(string id)
        {
            OrderDetails_Guest orderDetails_Guest = db.OrderDetails_Guest.Find(id);
            if (orderDetails_Guest == null)
            {
                return NotFound();
            }

            db.OrderDetails_Guest.Remove(orderDetails_Guest);
            db.SaveChanges();

            return Ok(orderDetails_Guest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetails_GuestExists(string id)
        {
            return db.OrderDetails_Guest.Count(e => e.OrderID == id) > 0;
        }
    }
}