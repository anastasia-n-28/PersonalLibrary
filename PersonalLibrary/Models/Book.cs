using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    /// <summary>
    /// Представляє книгу в персональній бібліотеці.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Отримує або встановлює назву книги.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Отримує або встановлює автора(-ів) книги.
        /// </summary>
        public string? Authors { get; set; }
        /// <summary>
        /// Отримує або встановлює видавництво книги.
        /// </summary>
        public string? Publisher { get; set; }
        /// <summary>
        /// Отримує або встановлює рік видання книги.
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Отримує або встановлює ISBN книги.
        /// </summary>
        public string? ISBN { get; set; }
        /// <summary>
        /// Отримує або встановлює країну походження книги.
        /// </summary>
        public string? Origin { get; set; }
        /// <summary>
        /// Отримує або встановлює статус книги (наприклад, доступна, видана).
        /// </summary>
        public BookStatus Status { get; set; }
        /// <summary>
        /// Отримує або встановлює оцінку користувача для книги.
        /// </summary>
        public UserRating? Rating { get; set; }

        /// <summary>
        /// Отримує основну інформацію про книгу.
        /// </summary>
        /// <returns>Строка, що містить назву, автора, рік, видавництво та ISBN книги.</returns>
        public string GetInfo() =>
            $"{Title} by {Authors} ({Year}), {Publisher}, ISBN: {ISBN}";

        /// <summary>
        /// Отримує основну інформацію про книгу як рядок.
        /// </summary>
        public string Info => GetInfo();

        /// <summary>
        /// Встановлює статус книги.
        /// </summary>
        /// <param name="status">Новий статус книги.</param>
        public void SetStatus(BookStatus status) => Status = status;

        /// <summary>
        /// Встановлює оцінку користувача для книги.
        /// </summary>
        /// <param name="rating">Оцінка користувача та відгук.</param>
        public void SetRating(UserRating rating) => Rating = rating;

        /// <summary>
        /// Отримує країну походження книги.
        /// </summary>
        /// <returns>Країна походження, або порожній рядок, якщо не вказано.</returns>
        public string GetOrigin() => Origin ?? string.Empty;

        /// <summary>
        /// Перевіряє властивості книги.
        /// </summary>
        /// <returns>Порожній рядок, якщо книга валідна, інакше рядок з повідомленнями про помилки.</returns>
        public string Validate()
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(Title)) error += "Назва обов'язкова\n";
            if (string.IsNullOrWhiteSpace(Authors)) error += "Authors are required.\n";
            if (string.IsNullOrWhiteSpace(Publisher)) error += "Publisher is required.\n";
            if (Year <= 0) error += "Year must be greater than 0.\n";
            if (string.IsNullOrWhiteSpace(ISBN)) error += "ISBN is required.\n";
            return error;
        }

        /// <summary>
        /// Отримує текстове представлення статусу книги.
        /// </summary>
        public string StatusText => Status.ToString();

        /// <summary>
        /// Отримує текстове представлення оцінки користувача.
        /// </summary>
        public string RatingText => Rating != null && Rating.Score >= 1 && Rating.Score <= 5 ? Rating.Score.ToString() : "";
    }
}
