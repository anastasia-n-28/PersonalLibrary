using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    public class LibrarySection
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new();

        public List<Book> GetBooks() => Books;

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }

        public string Validate()
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(Name)) error += "Section name is required.\n";
            return error;
        }
    }
}
