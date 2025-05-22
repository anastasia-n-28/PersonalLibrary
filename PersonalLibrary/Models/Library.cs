using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace PersonalLibrary.Models
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

        public void SerializeData(string path)
        {
            string json = JsonSerializer.Serialize(Sections, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public void DeserializeData(string path)
        {
            if (!File.Exists(path)) return;
            string json = File.ReadAllText(path);
            var sections = JsonSerializer.Deserialize<List<LibrarySection>>(json);
            if (sections != null)
                Sections = sections;
        }

        public void AddBook(string sectionName, Book book)
        {
            var section = Sections.FirstOrDefault(s => s.Name == sectionName);
            if (section != null)
                section.Books.Add(book);
        }

        public void RemoveBook(string sectionName, Book book)
        {
            var section = Sections.FirstOrDefault(s => s.Name == sectionName);
            if (section != null)
                section.Books.Remove(book);
        }

        public void AddSection(string sectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                throw new ArgumentException("Назва розділу не може бути порожньою");

            if (Sections.Any(s => s.Name.Equals(sectionName, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Розділ з такою назвою вже існує");

            Sections.Add(new LibrarySection { Name = sectionName });
        }

        public void RemoveSection(string sectionName)
        {
            var section = Sections.FirstOrDefault(s => s.Name.Equals(sectionName, StringComparison.OrdinalIgnoreCase));
            if (section != null)
            {
                Sections.Remove(section);
            }
        }

        public void RenameSection(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Нова назва розділу не може бути порожньою");

            if (Sections.Any(s => s.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Розділ з такою назвою вже існує");

            var section = Sections.FirstOrDefault(s => s.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase));
            if (section != null)
            {
                section.Name = newName;
            }
        }
    }
}
