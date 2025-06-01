using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    public class Book
    {
        public string? Title { get; set; }
        public string? Authors { get; set; }
        public string? Publisher { get; set; }
        public int Year { get; set; }
        public string? ISBN { get; set; }
        public string? Origin { get; set; }
        public BookStatus Status { get; set; }
        public UserRating? Rating { get; set; }
        public string GetInfo() =>
            $"{Title} by {Authors} ({Year}), {Publisher}, ISBN: {ISBN}";
        public string Info => GetInfo();
        public void SetStatus(BookStatus status) => Status = status;
        public void SetRating(UserRating rating) => Rating = rating;
        public string GetOrigin() => Origin ?? string.Empty;
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
        public string StatusText => Status.ToString();
        public string RatingText => Rating != null ? new string('★', Rating.Score) + new string('☆', 5 - Rating.Score) : "";
    }

}
