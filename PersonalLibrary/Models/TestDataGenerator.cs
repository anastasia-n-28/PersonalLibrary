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
            var history = new LibrarySection { Name = "History" };
            var technology = new LibrarySection { Name = "Technology" };
            var travel = new LibrarySection { Name = "Travel" };

            // Fiction
            fiction.Books.AddRange(
            [
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
                },
                new Book
                {
                    Title = "Fahrenheit 451",
                    Authors = "Ray Bradbury",
                    Publisher = "Ballantine Books",
                    Year = 1953,
                    ISBN = "978-0-345-34296-6",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 5, Review = "A chilling look at censorship" }
                },
                new Book
                {
                    Title = "Dune",
                    Authors = "Frank Herbert",
                    Publisher = "Chilton Books",
                    Year = 1965,
                    ISBN = "978-0-441-17271-9",
                    Origin = "USA",
                    Status = BookStatus.Reading,
                    Rating = new UserRating { Score = 5, Review = "Epic sci-fi world-building" }
                },
                new Book
                {
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    Authors = "Douglas Adams",
                    Publisher = "Pan Books",
                    Year = 1979,
                    ISBN = "978-0-345-39180-3",
                    Origin = "UK",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 4, Review = "Hilarious and absurd" }
                },
                new Book
                {
                    Title = "The Lord of the Rings",
                    Authors = "J.R.R. Tolkien",
                    Publisher = "George Allen & Unwin",
                    Year = 1954,
                    ISBN = "978-0-618-26025-6",
                    Origin = "UK",
                    Status = BookStatus.Finished,
                    Rating = new UserRating { Score = 5, Review = "A monumental fantasy epic" }
                },
                new Book
                {
                    Title = "Harry Potter and the Sorcerer's Stone",
                    Authors = "J.K. Rowling",
                    Publisher = "Bloomsbury Publishing",
                    Year = 1997,
                    ISBN = "978-0-7475-3269-9",
                    Origin = "UK",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 4, Review = "A magical start to a series" }
                },
                new Book
                {
                    Title = "The Hunger Games",
                    Authors = "Suzanne Collins",
                    Publisher = "Scholastic Press",
                    Year = 2008,
                    ISBN = "978-0-439-02352-8",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 4, Review = "Gripping and intense" }
                }
            ]);

            // Science
            science.Books.AddRange(
            [
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
                },
                new Book
                {
                    Title = "Cosmos",
                    Authors = "Carl Sagan",
                    Publisher = "Random House",
                    Year = 1980,
                    ISBN = "978-0-345-33135-9",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 5, Review = "Inspiring and educational" }
                },
                new Book
                {
                    Title = "Sapiens: A Brief History of Humankind",
                    Authors = "Yuval Noah Harari",
                    Publisher = "Harper",
                    Year = 2014,
                    ISBN = "978-0-06-231609-7",
                    Origin = "Israel",
                    Status = BookStatus.Reading,
                    Rating = new UserRating { Score = 5, Review = "A grand tour of human history" }
                },
                new Book
                {
                    Title = "The Immortal Life of Henrietta Lacks",
                    Authors = "Rebecca Skloot",
                    Publisher = "Crown Publishing Group",
                    Year = 2010,
                    ISBN = "978-1-4000-5210-9",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 5, Review = "Powerful and moving story" }
                }
            ]);

            // Classics
            classics.Books.AddRange(
            [
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
                    Title = "Захар Беркут",
                    Authors = "Іван Франко",
                    Publisher = "Львівська газета",
                    Year = 1883,
                    ISBN = "978-966-03-1234-5",
                    Origin = "Ukraine",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 5, Review = "Історична повість" }
                },
                new Book
                {
                    Title = "Тіні забутих предків",
                    Authors = "Михайло Коцюбинський",
                    Publisher = "Дніпро",
                    Year = 1911,
                    ISBN = "978-966-06-1234-6",
                    Origin = "Ukraine",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 4, Review = "Магічний реалізм" }
                },
                new Book
                {
                    Title = "To Kill a Mockingbird",
                    Authors = "Harper Lee",
                    Publisher = "J.B. Lippincott & Co.",
                    Year = 1960,
                    ISBN = "978-0-06-112008-4",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 5, Review = "Pulitzer Prize winner" }
                },
                new Book
                {
                    Title = "Pride and Prejudice",
                    Authors = "Jane Austen",
                    Publisher = "T. Egerton, Whitehall",
                    Year = 1813,
                    ISBN = "978-0-19-283355-2",
                    Origin = "UK",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 5, Review = "Classic romance" }
                },
                new Book
                {
                    Title = "War and Peace",
                    Authors = "Leo Tolstoy",
                    Publisher = "The Russian Messenger",
                    Year = 1869,
                    ISBN = "978-0-14-044793-4",
                    Origin = "Russia",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 4, Review = "A long but rewarding read" }
                },
                new Book
                {
                    Title = "The Odyssey",
                    Authors = "Homer",
                    Publisher = "Penguin Classics",
                    Year = -800,
                    ISBN = "978-0-14-026886-6",
                    Origin = "Ancient Greece",
                    Status = BookStatus.Finished,
                    Rating = new UserRating { Score = 5, Review = "An epic journey" }
                },
                new Book
                {
                    Title = "Don Quixote",
                    Authors = "Miguel de Cervantes",
                    Publisher = "Francisco de Robles",
                    Year = 1605,
                    ISBN = "978-0-06-093434-7",
                    Origin = "Spain",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 4, Review = "A satirical masterpiece" }
                }
            ]);

            // History
            history.Books.AddRange(
            [
                new Book
                {
                    Title = "A People's History of the United States",
                    Authors = "Howard Zinn",
                    Publisher = "Harper & Row",
                    Year = 1980,
                    ISBN = "978-0060838652",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 5, Review = "Essential reading" }
                },
                new Book
                {
                    Title = "The Guns of August",
                    Authors = "Barbara W. Tuchman",
                    Publisher = "Macmillan",
                    Year = 1962,
                    ISBN = "978-034547609 Guns",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 4, Review = "Detailed account of WWI start" }
                }
            ]);

            // Technology
            technology.Books.AddRange(
            [
                new Book
                {
                    Title = "Code: The Hidden Language of Computer Hardware and Software",
                    Authors = "Charles Petzold",
                    Publisher = "Microsoft Press",
                    Year = 1999,
                    ISBN = "978-0735611313",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 5, Review = "Great introduction to computing" }
                },
                new Book
                {
                    Title = "The Pragmatic Programmer",
                    Authors = "Andrew Hunt, David Thomas",
                    Publisher = "Addison-Wesley Professional",
                    Year = 1999,
                    ISBN = "978-0201616224",
                    Origin = "USA",
                    Status = BookStatus.Reading,
                    Rating = new UserRating { Score = 5, Review = "Timeless advice for developers" }
                }
            ]);

            // Travel
            travel.Books.AddRange(
            [
                new Book
                {
                    Title = "Into the Wild",
                    Authors = "Jon Krakauer",
                    Publisher = "Anchor Books",
                    Year = 1996,
                    ISBN = "978-0385486804",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 4, Review = "A compelling true story" }
                },
                new Book
                {
                    Title = "The Geography of Bliss",
                    Authors = "Eric Weiner",
                    Publisher = "Twelve",
                    Year = 2008,
                    ISBN = "978-0446698169",
                    Origin = "USA",
                    Status = BookStatus.Available,
                    Rating = new UserRating { Score = 4, Review = "Funny and insightful" }
                }
            ]);

            var library = new Library();
            library.Sections.AddRange([fiction, science, classics, history, technology, travel]);

            return library;
        }
    }
}
