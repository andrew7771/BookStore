using RESTAPI.Models;
using Web_Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Web_Application.Controllers
{
    public class CategoriesController : Controller
    {
        private HttpClient client = new HttpClient();
        private HttpConfig conf = new HttpConfig();

        [Authorize]
        public ActionResult Index()
        {            
            List<Category> categories = new List<Category>();
            List<CategoryBook> categoryBooks = new List<CategoryBook>();
            List<BookCategotiesViewModel> model = new List<BookCategotiesViewModel>();
            conf.SetClientGETSettings(client);

            HttpResponseMessage response = client.GetAsync("api/Categories").Result;
            if (response.IsSuccessStatusCode)
            {
                categories = response.Content.ReadAsAsync<List<Category>>().Result;
            }

            HttpResponseMessage responseCategoryBooks = client.GetAsync("api/CategoryBooks").Result;
            if (responseCategoryBooks.IsSuccessStatusCode)
            {
                categoryBooks = responseCategoryBooks.Content.ReadAsAsync<List<CategoryBook>>().Result;
            }

            if (categories != null)
            {
                foreach (var category in categories)
                {
                    model.Add(new BookCategotiesViewModel()
                    {
                        Category = category,
                        CategoryBooks = categoryBooks.FindAll(c => c.CategoryId == category.CategoryId)
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
        public ActionResult Create(Category category)
        {
            conf.SetClientGETSettings(client);
            client.PostAsJsonAsync<Category>("api/Categories", category).
                ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Book> books = new List<Book>();
            Category category = new Category();
            conf.SetClientGETSettings(client);

            HttpResponseMessage response = client.GetAsync("api/Categories/" + id + "/1").Result;
            if (response.IsSuccessStatusCode)
            {
                category = response.Content.ReadAsAsync<Category>().Result;
            }
            if (category == null)
            {
                return HttpNotFound();
            }
            HttpResponseMessage responseCategories = client.GetAsync("api/Books/").Result;
            if (responseCategories.IsSuccessStatusCode)
            {
                books = responseCategories.Content.ReadAsAsync<List<Book>>().Result;
            }
            ViewBag.BookId = new MultiSelectList(books, "BookId", "Name");
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category, int?[] BookId)
        {
            client.BaseAddress = new Uri(conf.getAdress());
            client.PutAsJsonAsync<Category>($"api/Category/{category.CategoryId}", category).
                ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            if (BookId != null)
            {
                for (int i = 0; i < BookId.Length; i++)
                {
                    CategoryBook categoryBook = new CategoryBook()
                    {
                        CategoryId = category.CategoryId,
                        BookId = (int)BookId[i]
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
            Category category = new Category();
            conf.SetClientGETSettings(client);

            HttpResponseMessage response = client.GetAsync("api/Categories/" + id + "/0").Result;
            if (response.IsSuccessStatusCode)
            {
                category = response.Content.ReadAsAsync<Category>().Result;
            }
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = new Book();
            conf.SetClientGETSettings(client);
            client.DeleteAsync($"api/Categories/{id}").
                ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            HttpResponseMessage response = client.GetAsync("api/Categories/" + id + "/1").Result;
            if (response.IsSuccessStatusCode)
            {
                book = response.Content.ReadAsAsync<Book>().Result;
            }
            return RedirectToAction("Index");
        }       
    }
}