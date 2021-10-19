using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectModule25
{
    public class BookRepository
    {
        private AppContext context;

        public BookRepository(AppContext appContext) => context = appContext;

        public Book GetById(int id) => context.Books.FirstOrDefault(u => u.Id == id);

        public List<Book> GetAll() => context.Books.ToList();

        public void Add(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void AddMany(params Book[] books)
        {
            foreach (var b in books)
                context.Books.Add(b);
            context.SaveChanges();
        }

        public void Remove(Book book)
        {
            context.Books.Remove(book);
            context.SaveChanges();
        }

        public void UpdatePublicationYear(int id, int newYear)
        {
            var bookToAlter = context.Books.FirstOrDefault(b => b.Id == id);
            if (bookToAlter != null)
            {
                bookToAlter.PublicationYear = newYear;
                context.SaveChanges();
            }
        }

        public List<Book> ByGenreAndTimeSpan(string genre, int from, int to)
        {
            int floor = Math.Min(from, to);
            int ceiling = Math.Max(from, to);

            return context.Books.Where(b => b.Genre == genre && b.PublicationYear <= ceiling
                && b.PublicationYear >= floor).ToList();
        }

        public int CountByAuthor(string authorName) =>
            context.Books.Count(b => b.Author == authorName);

        public int CountByGenre(string genre) =>
            context.Books.Count(b => b.Genre == genre);

        public bool BookExists(string bookName, string authorName) =>
            context.Books.Any(b => b.Author == authorName && b.Name == bookName);

        public Book Newest() => 
            context.Books.OrderBy(b => b.PublicationYear).ThenBy(b => b.Id).Last();

        public List<Book> GetSortedByName() => 
            context.Books.OrderBy(b => b.Name).ToList();

        public List<Book> GetSortedByYear() => 
            context.Books.OrderByDescending(b => b.PublicationYear).ToList();
    }
}