using Microsoft.EntityFrameworkCore;

namespace ProjectModule25
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-GDH9VH5\SQLEXPRESS;Database=Module25;Trusted_Connection=True;");
    }
}
