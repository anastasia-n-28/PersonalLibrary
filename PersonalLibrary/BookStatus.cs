using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary
{
    public class BookStatus
    {
        public bool IsAvailable { get; set; }
        public string Condition { get; set; }
        public void SetAvailability(bool status) => IsAvailable = status;
    }

}