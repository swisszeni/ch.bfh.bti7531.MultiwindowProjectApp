using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiwindowTestApp.Models
{
    public class UserAgendaDay
    {
        public DateTime Date { get; set; }
        public User User { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }

        public UserAgendaDay()
        {
            Appointments = new ObservableCollection<Appointment>();
        }
    }
}
