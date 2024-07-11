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
using ProductAPI.Context;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    public class ProductController : ApiController
    {
        private ProductContext db = new ProductContext();

        // GET: api/Product
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var result = db.Products.Select(product => new
                {
                    product.name,
                    product.price,
                    product.quantity,
                    product.description
                }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // GET: api/Product/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var result = db.Products.Find(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Product
        [ResponseType(typeof(Product))]
        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.id }, product);
        }

        // DELETE: api/Product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.id == id) > 0;
        }
    }
}