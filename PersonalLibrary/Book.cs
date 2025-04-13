using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary
{
    public class Book
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public BookStatus Status { get; set; }
        public UserRating Rating { get; set; }
        public string Origin { get; set; }

        public string GetInfo() =>
            $"{Title} by {Authors} ({Year}), {Publisher}, ISBN: {ISBN}";

        public void SetStatus(BookStatus status) => Status = status;
        public void SetRating(UserRating rating) => Rating = rating;
        public string GetOrigin() => Origin;
    }

}
