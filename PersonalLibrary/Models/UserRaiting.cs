using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    /// <summary>
    /// Представляє оцінку користувача для книги.
    /// </summary>
    public class UserRating
    {
        /// <summary>
        /// Отримує або встановлює оцінку, яку дав користувач (наприклад, 1-5).
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Отримує або встановлює текст відгуку користувача.
        /// </summary>
        public string Review { get; set; } = string.Empty;

        /// <summary>
        /// Встановлює оцінку та відгук для оцінки.
        /// </summary>
        /// <param name="score">Оцінка, яку дав користувач.</param>
        /// <param name="review">Текст відгуку користувача.</param>
        public void SetRating(int score, string review)
        {
            Score = score;
            Review = review;
        }
    }
}
