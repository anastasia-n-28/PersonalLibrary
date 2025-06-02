using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    /// <summary>
    /// Представляє розділ у бібліотеці.
    /// </summary>
    public class LibrarySection
    {
        /// <summary>
        /// Отримує або встановлює назву розділу бібліотеки.
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Отримує або встановлює список книг у цьому розділі.
        /// </summary>
        public List<Book> Books { get; set; } = [];

        /// <summary>
        /// Отримує список книг у цьому розділі.
        /// </summary>
        /// <returns>Список книг.</returns>
        public List<Book> GetBooks() => Books;

        /// <summary>
        /// Додає книгу до цього розділу.
        /// </summary>
        /// <param name="book">Книга для додавання.</param>
        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        /// <summary>
        /// Видаляє книгу з цього розділу.
        /// </summary>
        /// <param name="book">Книга для видалення.</param>
        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }

        /// <summary>
        /// Перевіряє властивості розділу бібліотеки.
        /// </summary>
        /// <returns>Порожній рядок, якщо розділ валідний, інакше рядок з повідомленнями про помилки.</returns>
        public string Validate()
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(Name)) error += "Section name is required.\n";
            return error;
        }
    }
}
