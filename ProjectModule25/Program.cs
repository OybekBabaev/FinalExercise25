using System;
using System.Linq;

namespace ProjectModule25
{
    class Program
    {
        static void Main()
        {
            using var db = new AppContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var bookRepo = new BookRepository(db);
            var userRepo = new UserRepository(db);

            var book1 = new Book { Name = "The Big Rizotto", Genre = "Novel", PublicationYear = 2003, Author = "Lautaro Novorizontino dos Santos Dolores" };
            var book2 = new Book { Name = "7 Types of Insanely Hilarious Jokes", Genre = "Non-Fiction", PublicationYear = 2019, Author = "Ken Insincero" };
            var book3 = new Book { Name = "Pro C# 10 and .NET 6", PublicationYear = 2022, Genre = "Science", Author = "Oybek Babaev" };
            var book4 = new Book { PublicationYear = 1993, Genre = "Science", Name = "Mathematik fuer Wirtschaftswissenschaftler", Author = "Stefan Waltz" };
            var book5 = new Book { Name = "Plutonia", PublicationYear = 1924, Author = "Vladimir Obruchev", Genre = "Science Fiction" };
            var book6 = new Book { Name = "Interpreter\'s Comrades", PublicationYear = 2010, Author = "Carlos Ressureccion Ledesma", Genre = "Novel" };
            var book7 = new Book { Name = "Linguistica 101: A Newly Idiomatic Approach", PublicationYear = 2008, Author = "Oybek Babaev", Genre = "Science" };
            var book8 = new Book { Name = "Serenity Falls", Genre = "Fantasy", Author = "Carlos Ressureccion Ledesma", PublicationYear = 2022 };

            bookRepo.AddMany(book1, book2, book3, book4, book5, book6, book7, book8);

            var user1 = new User { Name = "Gregorio", Email = "gregTheJerk@yahoo.com" };
            var user2 = new User { Name = "Oibes", Email = "oibesDeLaMusterio@gmail.com" };
            var user3 = new User { Name = "Dan the Man" };
            var user4 = new User { Name = "Gremio", Email = "gremio618novoriz@yahoo.com" };

            userRepo.AddMany(user1, user2, user3, user4);

            user1.Books.AddRange(new[] { book3, book4, book5 });
            user2.Books.AddRange(new[] { book1, book4, book6, book7 });
            user3.Books.Add(book2);
            user4.Books.AddRange(new[] { book2, book3, book6 });

            db.SaveChanges();

            //тестирую методы из репозиториев
            Console.WriteLine("Science books from 1990 to 2022: ");
            var scienceBooks = bookRepo.ByGenreAndTimeSpan("Science", 1990, 2022)
                .OrderBy(b => b.PublicationYear);
            foreach (var b in scienceBooks) Console.WriteLine("{0}, {1}",
                b.Name, b.PublicationYear);

            Console.WriteLine("\nBooks authored by Oybek Babaev: {0}",
                bookRepo.CountByAuthor("Oybek Babaev"));

            Console.WriteLine("\nIs there a book called \"Gremio\" by F. Nolito? " +
                bookRepo.BookExists("Gremio", "Fernando Nolito"));

            var newest = bookRepo.Newest();
            Console.WriteLine("\nThe newest book in the repository: {0}, ({1}, {2}) (ID #{3})",
                newest.Name, newest.Author, newest.PublicationYear, newest.Id);

            Console.WriteLine("\nActive users and the books they read:");
            foreach (var u in db.Users)
            {
                Console.WriteLine("\t{0} has {1} book(s)",
                    u.Name, userRepo.BookCount(u));
                var userBooks = u.Books;
                foreach (var b in userBooks) Console.WriteLine("\t\t{0}, {1}",
                    b.Name, b.PublicationYear);
            }

            Console.WriteLine("\nAll books in the repository, starting from newer ones:");
            foreach (var b in bookRepo.GetSortedByYear()) Console.WriteLine(">>> {0} ({1})",
                b.Name, b.PublicationYear);
        }
    }
}
