using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary
{
    public class Library
    {
        public List<LibrarySection> Sections { get; set; } = new();

        public List<Book> FindBooks(string title, string author, string publisher)
        {
            return Sections
                .SelectMany(s => s.Books)
                .Where(b =>
                    b.Title.Contains(title, StringComparison.OrdinalIgnoreCase) &&
                    b.Authors.Contains(author, StringComparison.OrdinalIgnoreCase) &&
                    b.Publisher.Contains(publisher, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void InventoryCheck()
        {
            foreach (var section in Sections)
            {
                foreach (var book in section.Books)
                {
                    Console.WriteLine(book.GetInfo());
                }
            }
        }
    }
}
