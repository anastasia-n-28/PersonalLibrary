using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    public class UserRating
    {
        public int Score { get; set; }
        public string Review { get; set; }
        public void SetRating(int score, string review)
        {
            Score = score;
            Review = review;
        }
    }
}
