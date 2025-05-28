using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    public enum BookStatus
    {
        Available, // Наявна
        Missing,   // Відсутня
        Reserved,  // Зарезервована
        Lost,      // Втрачена
        Reading,   // Читається
        Finished   // Прочитана
    }
}