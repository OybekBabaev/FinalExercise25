using System.Collections.Generic;

namespace ProjectModule25
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublicationYear { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public List<User> Owners { get; set; }
    }
}
