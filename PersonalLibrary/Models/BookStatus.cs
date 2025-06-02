using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    /// <summary>
    /// Представляє статус книги в бібліотеці.
    /// </summary>
    public enum BookStatus
    {
        /// <summary>
        /// Книга доступна.
        /// </summary>
        Available,
        /// <summary>
        /// Книга відсутня.
        /// </summary>
        Missing,
        /// <summary>
        /// Книга зарезервована.
        /// </summary>
        Reserved,
        /// <summary>
        /// Книга втрачена.
        /// </summary>
        Lost,
        /// <summary>
        /// Книга в даний момент читається.
        /// </summary>
        Reading,
        /// <summary>
        /// Книга прочитана.
        /// </summary>
        Finished
    }
}