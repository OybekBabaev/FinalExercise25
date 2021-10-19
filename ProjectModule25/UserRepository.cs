using System.Collections.Generic;
using System.Linq;

namespace ProjectModule25
{
    public class UserRepository
    {
        private AppContext context;

        public UserRepository(AppContext appContext) => context = appContext;

        public User GetById(int id) => context.Users.FirstOrDefault(u => u.Id == id);

        public List<User> GetAll() => context.Users.ToList();

        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void AddMany(params User[] users)
        {
            foreach (var u in users)
                context.Users.Add(u);
            context.SaveChanges();
        }

        public void Remove(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var userToAlter = context.Users.FirstOrDefault(u => u.Id == id);
            if (userToAlter != null)
            {
                userToAlter.Name = newName;
                context.SaveChanges();
            }            
        }

        public bool HasBook(User user, Book book) => user.Books.Any(b => b.Equals(book));

        public int BookCount(User user) => user.Books.Count();
    }
}
