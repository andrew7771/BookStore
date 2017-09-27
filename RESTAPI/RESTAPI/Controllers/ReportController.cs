using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    public class ReportController : ApiController
    {
        private BookStoreDbContext db = new BookStoreDbContext();
        public Dictionary<string,int> Get()
        {
            Dictionary<string, int> categoryAmount = new Dictionary<string, int>();
            categoryAmount.Add("[Unknown]", 0);

            foreach (var category in db.Categories)
            {
                int counter = db.CategoryBooks.Where(c => c.Category.Name == category.Name).Count();
                if (counter != 0)
                {
                    categoryAmount.Add(category.Name, 0);
                    categoryAmount[category.Name] = counter;
                }
            }
            int booksWithoutCategories = db.Books.Where(b => b.CategoryBooks.Count() == 0).Count();
            categoryAmount["[Unknown]"] = booksWithoutCategories;
            return categoryAmount;
        }
    }
}
