using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace PersonalLibrary.Models
{
    /// <summary>
    /// Представляє колекцію книг і управляє операціями бібліотеки.
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Отримує або встановлює список розділів у бібліотеці.
        /// </summary>
        public List<LibrarySection> Sections { get; set; } = [];

        // Cache JsonSerializerOptions
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

        /// <summary>
        /// Знаходить книги у бібліотеці на основі наданих критеріїв.
        /// </summary>
        /// <param name="title">Необов'язкова назва для пошуку.</param>
        /// <param name="author">Необов'язковий автор для пошуку.</param>
        /// <param name="publisher">Необов'язкове видавництво для пошуку.</param>
        /// <returns>Перелік книг, що відповідають критеріям.</returns>
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

        /// <summary>
        /// Виконує інвентарну перевірку книг.
        /// </summary>
        /// Цей метод поки що виводить інформацію про книги у консоль. Потрібно реалізувати для фактичної інвентарної перевірки.
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

        /// <summary>
        /// Сериалізує дані бібліотеки у файл JSON.
        /// </summary>
        /// <param name="path">Шлях до файлу, де будуть збережені дані.</param>
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

        /// <summary>
        /// Десериалізує дані бібліотеки з файлу JSON.
        /// </summary>
        /// <param name="path">Шлях до файлу JSON.</param>
        /// <returns>Десериалізований об'єкт бібліотеки, або null, якщо файл не існує або декодування не вдалося.</returns>
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

        /// <summary>
        /// Додає книгу до вказаного розділу.
        /// </summary>
        /// <param name="sectionName">Назва розділу, до якого додається книга.</param>
        /// <param name="book">Книга для додавання.</param>
        public void AddBook(string sectionName, Book book)
        {
            var section = Sections.FirstOrDefault(s => s.Name == sectionName);
            section?.Books.Add(book);
        }

        /// <summary>
        /// Видаляє книгу з вказаного розділу.
        /// </summary>
        /// <param name="sectionName">Назва розділу, з якого видаляється книга.</param>
        /// <param name="book">Книга для видалення.</param>
        public void RemoveBook(string sectionName, Book book)
        {
            var section = Sections.FirstOrDefault(s => s.Name == sectionName);
            section?.Books.Remove(book);
        }

        /// <summary>
        /// Додає новий розділ до бібліотеки.
        /// </summary>
        /// <param name="sectionName">Назва нового розділу.</param>
        /// <exception cref="ArgumentException">Кидає виняток, якщо назва розділу порожня або розділ з такою назвою вже існує.</exception>
        public void AddSection(string sectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                throw new ArgumentException("Назва розділу не може бути порожньою");

            if (Sections.Any(s => s.Name.Equals(sectionName, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Розділ з такою назвою вже існує");

            Sections.Add(new LibrarySection { Name = sectionName });
        }

        /// <summary>
        /// Видаляє розділ з бібліотеки.
        /// </summary>
        /// <param name="sectionName">Назва розділу для видалення.</param>
        public void RemoveSection(string sectionName)
        {
            var section = Sections.FirstOrDefault(s => s.Name.Equals(sectionName, StringComparison.OrdinalIgnoreCase));
            if (section != null)
            {
                Sections.Remove(section);
            }
        }

        /// <summary>
        /// Перейменовує існуючий розділ.
        /// </summary>
        /// <param name="oldName">Поточна назва розділу.</param>
        /// <param name="newName">Нова назва для розділу.</param>
        /// <exception cref="ArgumentException">Кидає виняток, якщо нова назва порожня або розділ з такою назвою вже існує.</exception>
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
