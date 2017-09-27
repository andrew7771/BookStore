using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RESTAPI.Models
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext()
            : base("name=BookStoreConnection") { }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryBook> CategoryBooks { get; set; }
    }
}