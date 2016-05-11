using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiwindowTestApp.Models
{
    public class Appointment
    {
        public DateTime Start { get; set;  }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
    }
}
