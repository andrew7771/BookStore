using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfCreating { get; set; }

        public virtual ICollection<CategoryBook> CategoryBooks { get; set; }
    }

    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public virtual  ICollection<CategoryBook> CategoryBooks { get; set; }
    }

    public class CategoryBook
    {        
        [Key, Column(Order = 1)]
        public int BookId { get; set; }
        [Key, Column(Order = 2)]
        public int CategoryId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Category Category { get; set; }
    }

    public class CategoryBooksViewModel
    {
        public Book Book { get; set; }
        public List<CategoryBook> CategoryBooks { get; set; }
    }

    public class BookCategotiesViewModel
    {
        public Category Category { get; set; }
        public List<CategoryBook> CategoryBooks { get; set; }
    }
}