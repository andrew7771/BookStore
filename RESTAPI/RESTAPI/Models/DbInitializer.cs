using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RESTAPI.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<BookStoreDbContext>
    {
        protected override void Seed(BookStoreDbContext context)
        {
            List<Category> categories = new List<Category>()
            {
                new Category() {Name="Ужасы", DateOfCreating= DateTime.Now },
                new Category() {Name="Программирование", DateOfCreating= DateTime.Now },
                new Category() {Name="Роман", DateOfCreating= DateTime.Now, },
                new Category() {Name="Фентези", DateOfCreating= DateTime.Now, },
                new Category() {Name="Детская литература", DateOfCreating= DateTime.Now, },
                new Category() {Name="Повесть", DateOfCreating= DateTime.Now, },
                new Category() {Name="Триллер", DateOfCreating= DateTime.Now, },
                new Category() {Name="Боевик", DateOfCreating= DateTime.Now, },
                new Category() {Name="Баллада", DateOfCreating= DateTime.Now, },

            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            List<Book> books = new List<Book>()
            {
                new Book() {Author="Стивен Кинг", ISBN="а41341245321", Name="Оно" },
                new Book() {Author="Стивен Кинг", ISBN="134542364455", Name="Темная башня" },
                new Book() {Author="Джеффри Рихтер", ISBN="qfg2t3rg3t233", Name="CLR via C#" },
                new Book() {Author="Гелберт Шилдт", ISBN="fwgh44etrwthg", Name="C# для профессионалов"  },
                new Book() {Author="Агата Кристи", ISBN="wgh44grewhe", Name="Десять негритят" },
                new Book() {Author="Тарас Шевченко", ISBN="fwegrehewgg", Name="КОбзарь" },
                new Book() {Author="Сергей Есенин", ISBN="wgrhjh343tqwegw", Name="Черный человек" },
                new Book() {Author="Адександр Пушкин", ISBN="wryh4gtrt3", Name="Евгений Онегин" },
                new Book() {Author="Федор Достоевский", ISBN="h34gtwhg5rt", Name="Преступление и наказание" },
                new Book() {Author="Федор Достоевский", ISBN="twyh435gwt3ty", Name="Идиот" },
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();
            base.Seed(context);
        }
    }
}