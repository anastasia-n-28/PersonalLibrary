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
        public List<LibrarySection> Sections { get; set; } = [];

        // Cache JsonSerializerOptions
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

        public IEnumerable<Book> FindBooks(string? title, string? author, string? publisher)
        {
            var query = Sections.SelectMany(s => s.Books);

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(book => book.Title?.Contains(title, StringComparison.OrdinalIgnoreCase) ?? false);
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(book => book.Authors?.Contains(author, StringComparison.OrdinalIgnoreCase) ?? false);
            }

            if (!string.IsNullOrWhiteSpace(publisher))
            {
                query = query.Where(book => book.Publisher?.Contains(publisher, StringComparison.OrdinalIgnoreCase) ?? false);
            }

            return [.. query];
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
            try
            {
                string json = JsonSerializer.Serialize(this, _jsonSerializerOptions);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serialization error: {ex.Message}");
                throw;
            }
        }

        public static Library? DeserializeData(string path)
        {
            if (!File.Exists(path)) return null;

            try
            {
                string json = File.ReadAllText(path);
                var library = JsonSerializer.Deserialize<Library>(json, _jsonSerializerOptions);
                return library;
            }
            catch (JsonException jEx)
            {
                Console.WriteLine($"JSON deserialization error: {jEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return null;
            }
        }

        public void AddBook(string sectionName, Book book)
        {
            var section = Sections.FirstOrDefault(s => s.Name == sectionName);
            section?.Books.Add(book);
        }

        public void RemoveBook(string sectionName, Book book)
        {
            var section = Sections.FirstOrDefault(s => s.Name == sectionName);
            section?.Books.Remove(book);
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
