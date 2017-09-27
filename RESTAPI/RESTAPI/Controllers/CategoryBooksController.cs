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
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    public class CategoryBooksController : ApiController
    {
        private BookStoreDbContext db = new BookStoreDbContext();

        // GET: api/CategoryBooks
        public IQueryable<CategoryBook> GetCategoryBooks()
        {
            return db.CategoryBooks;
        }

        // GET: api/CategoryBooks/5
        [ResponseType(typeof(CategoryBook))]
        public IHttpActionResult GetCategoryBook(int id)
        {
            CategoryBook categoryBook = db.CategoryBooks.Find(id);
            if (categoryBook == null)
            {
                return NotFound();
            }

            return Ok(categoryBook);
        }

        // PUT: api/CategoryBooks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoryBook(int id, CategoryBook categoryBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoryBook.BookId)
            {
                return BadRequest();
            }

            db.Entry(categoryBook).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryBookExists(id))
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

        // POST: api/CategoryBooks
        [ResponseType(typeof(CategoryBook))]
        public IHttpActionResult PostCategoryBook(CategoryBook categoryBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CategoryBooks.Add(categoryBook);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CategoryBookExists(categoryBook.BookId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = categoryBook.BookId }, categoryBook);
        }

        // DELETE: api/CategoryBooks/5
        [ResponseType(typeof(CategoryBook))]
        public IHttpActionResult DeleteCategoryBook(int id)
        {
            CategoryBook categoryBook = db.CategoryBooks.Find(id);
            if (categoryBook == null)
            {
                return NotFound();
            }

            db.CategoryBooks.Remove(categoryBook);
            db.SaveChanges();

            return Ok(categoryBook);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryBookExists(int id)
        {
            return db.CategoryBooks.Count(e => e.BookId == id) > 0;
        }
    }
}