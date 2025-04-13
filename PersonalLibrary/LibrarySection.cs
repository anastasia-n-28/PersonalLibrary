using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary
{
    public class LibrarySection
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new();

        public List<Book> GetBooks() => Books;
    }
}
