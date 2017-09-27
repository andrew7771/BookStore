using RESTAPI.Models;
using Web_Application.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Web_Application.Controllers
{
    public class BooksController : Controller
    {
        private HttpClient client = new HttpClient();
        private HttpConfig conf = new HttpConfig(); 

        [Authorize]
        public ActionResult Index()
        {
            List<Book> books = new List<Book>();
            List<CategoryBook> categoryBooks = new List<CategoryBook>();
            List<CategoryBooksViewModel> model = new List<CategoryBooksViewModel>();
            conf.SetClientGETSettings(client);

            HttpResponseMessage response = client.GetAsync("api/Books").Result;
            if (response.IsSuccessStatusCode)
            {
                books = response.Content.ReadAsAsync<List<Book>>().Result;                
            }

            HttpResponseMessage responseCategoryBooks = client.GetAsync("api/CategoryBooks").Result;
            if (responseCategoryBooks.IsSuccessStatusCode)
            {
                categoryBooks = responseCategoryBooks.Content.ReadAsAsync<List<CategoryBook>>().Result;
            }

            if (books != null)
            {
                foreach (var book in books)
                {
                    model.Add(new CategoryBooksViewModel()
                    {
                        Book = book,
                        CategoryBooks = categoryBooks.FindAll(c => c.BookId == book.BookId)
                    });
                }
            }
            return View(model);
        }
        public ActionResult Create()
        {            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            conf.SetClientGETSettings(client);
            client.PostAsJsonAsync<Book>("api/Books", book).
                ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());       
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Category> categories = new List<Category>();
            Book book = new Book();
            conf.SetClientGETSettings(client);

            HttpResponseMessage response = client.GetAsync("api/Books/" + id + "/1").Result;
            if (response.IsSuccessStatusCode)
            {
                book = response.Content.ReadAsAsync<Book>().Result;
            }
            if (book == null)
            {
                return HttpNotFound();
            }
            HttpResponseMessage responseCategories = client.GetAsync("api/Categories/").Result;
            if (responseCategories.IsSuccessStatusCode)
            {
                categories = responseCategories.Content.ReadAsAsync<List<Category>>().Result;
            }
            ViewBag.CategoryId = new MultiSelectList(categories, "CategoryId", "Name");
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book, int?[] CategoryId)
        {
            client.BaseAddress = new Uri(conf.getAdress());
            client.PutAsJsonAsync<Book>($"api/Books/{book.BookId}", book).
                ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            if (CategoryId != null)
            {
                for (int i = 0; i < CategoryId.Length; i++)
                {
                    CategoryBook categoryBook = new CategoryBook()
                    {
                        BookId = book.BookId,
                        CategoryId = (int)CategoryId[i]
                    };
                    client.PostAsJsonAsync<CategoryBook>($"api/CategoryBooks/", categoryBook).
                    ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                }
            }                    
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = new Book();
            conf.SetClientGETSettings(client);

            HttpResponseMessage response = client.GetAsync("api/Books/" + id +"/0").Result;
            if (response.IsSuccessStatusCode)
            {
                book = response.Content.ReadAsAsync<Book>().Result;
            }
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = new Book();
            conf.SetClientGETSettings(client);

            client.DeleteAsync($"api/Books/{id}").
                ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            HttpResponseMessage response = client.GetAsync("api/Books/" + id + "/1").Result;
            if (response.IsSuccessStatusCode)
            {
                book = response.Content.ReadAsAsync<Book>().Result;
            }
            
            return RedirectToAction("Index");
        }       
    }
}