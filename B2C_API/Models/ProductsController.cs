using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace B2C_API.Models
{
    public class ProductsController : ApiController
    {
        private PhoneManagerEntities db = new PhoneManagerEntities();

        //GET: api/Products
        //public IQueryable<Products> GetProducts()
        //{
        //    return db.Products;
        //}
        private readonly string connectionString = "data source=(local)\\SQLEXPRESS;initial catalog=PhoneManager;integrated security=True;";
        [ResponseType(typeof(Products))]
        public IHttpActionResult GetProducts()
        {
            List<Products> products = new List<Products>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT *\r\nFROM Products\r\nINNER JOIN (\r\n  SELECT p.ProductID, SUM(d.Quantity) AS TotalQuantity\r\n  FROM Products p\r\n  INNER JOIN WarehouseReceiptDetails d ON p.ProductID = d.ProductID\r\n  GROUP BY p.ProductID\r\n) subquery ON Products.ProductID = subquery.ProductID;", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Products
                                {
                                    ProductID = reader.GetString(0),
                                    ProductName = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    Brand = reader.GetString(3),
                                    UnitPrice = reader.GetDecimal(4),
                                    ProImage = (byte[])reader["ProImage"],
                                    Quanity = (int)reader["TotalQuantity"]
                                }); ;
                            }
                        }
                    }
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Products/5
        [ResponseType(typeof(Products))]
        public IHttpActionResult GetProducts(string id)
        {
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducts(string id, Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != products.ProductID)
            {
                return BadRequest();
            }

            db.Entry(products).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(Products))]
        public IHttpActionResult PostProducts(Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(products);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductsExists(products.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = products.ProductID }, products);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Products))]
        public IHttpActionResult DeleteProducts(string id)
        {
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            db.Products.Remove(products);
            db.SaveChanges();

            return Ok(products);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsExists(string id)
        {
            return db.Products.Count(e => e.ProductID == id) > 0;
        }
    }
}