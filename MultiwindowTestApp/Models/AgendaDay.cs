using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiwindowTestApp.Models
{
    public class AgendaDay
    {
        public DateTime Date { get; set; }
        public Dictionary<string, ObservableCollection<Appointment>> AppointmentsPerUser { get; private set; }

        public AgendaDay()
        {
            AppointmentsPerUser = new Dictionary<string, ObservableCollection<Appointment>>();
        }
    }
}
