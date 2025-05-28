using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    public static class TestDataGenerator
    {
        public static Library Generate()
        {
            var fiction = new LibrarySection { Name = "Fiction" };
            var science = new LibrarySection { Name = "Science" };
            var classics = new LibrarySection { Name = "Classics" };

            // Fiction
            fiction.Books.AddRange(new[]
            {
            new Book
            {
                Title = "The Hobbit",
                Authors = "J.R.R. Tolkien",
                Publisher = "HarperCollins",
                Year = 1937,
                ISBN = "978-0-261-10221-7",
                Origin = "UK",
                Status = BookStatus.Available,
                Rating = new UserRating { Score = 5, Review = "Masterpiece" }
            },
            new Book
            {
                Title = "1984",
                Authors = "George Orwell",
                Publisher = "Secker & Warburg",
                Year = 1949,
                ISBN = "978-0-452-28423-4",
                Origin = "UK",
                Status = BookStatus.Missing,
                Rating = new UserRating { Score = 4, Review = "Thought-provoking" }
            },
            new Book
            {
                Title = "Brave New World",
                Authors = "Aldous Huxley",
                Publisher = "Chatto & Windus",
                Year = 1932,
                ISBN = "978-0-099-51324-4",
                Origin = "UK",
                Status = BookStatus.Available,
                Rating = new UserRating { Score = 4, Review = "Disturbingly relevant" }
            }
        });

            // Science
            science.Books.AddRange(new[]
            {
            new Book
            {
                Title = "A Brief History of Time",
                Authors = "Stephen Hawking",
                Publisher = "Bantam Books",
                Year = 1988,
                ISBN = "978-0-553-10953-5",
                Origin = "UK",
                Status = BookStatus.Available,
                Rating = new UserRating { Score = 5, Review = "Fascinating read" }
            },
            new Book
            {
                Title = "The Selfish Gene",
                Authors = "Richard Dawkins",
                Publisher = "Oxford University Press",
                Year = 1976,
                ISBN = "978-0-19-929115-1",
                Origin = "UK",
                Status = BookStatus.Missing,
                Rating = new UserRating { Score = 4, Review = "Groundbreaking ideas" }
            }
        });

            // Classics
            classics.Books.AddRange(new[]
            {
            new Book
            {
                Title = "Кобзар",
                Authors = "Тарас Шевченко",
                Publisher = "Основи",
                Year = 1840,
                ISBN = "978-966-500-123-4",
                Origin = "Ukraine",
                Status = BookStatus.Available,
                Rating = new UserRating { Score = 5, Review = "Класика української літератури" }
            },
            new Book
            {
                Title = "Майстер і Маргарита",
                Authors = "Михайло Булгаков",
                Publisher = "Азбука",
                Year = 1967,
                ISBN = "978-5-389-07437-0",
                Origin = "Russia",
                Status = BookStatus.Available,
                Rating = new UserRating { Score = 5, Review = "Містична та глибока історія" }
            }
        });

            var library = new Library();
            library.Sections.AddRange(new[] { fiction, science, classics });

            return library;
        }
    }
}
