using MultiwindowTestApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace MultiwindowTestApp
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            List<AgendaDay> mockupData = new List<AgendaDay>();

            AgendaDay day1 = new AgendaDay();
            day1.AppointmentsPerUser.Add("RZE", new ObservableCollection<Appointment>());
            day1.AppointmentsPerUser.Add("RKR", new ObservableCollection<Appointment>());
            day1.Date = new DateTime(2016,05,11);
            AgendaDay day2 = new AgendaDay();
            day2.AppointmentsPerUser.Add("RZE", new ObservableCollection<Appointment>());
            day2.AppointmentsPerUser.Add("RKR", new ObservableCollection<Appointment>());
            day2.Date = new DateTime(2016, 05, 12);
            AgendaDay day3 = new AgendaDay();
            day3.AppointmentsPerUser.Add("RZE", new ObservableCollection<Appointment>());
            day3.AppointmentsPerUser.Add("RKR", new ObservableCollection<Appointment>());
            day3.Date = new DateTime(2016, 05, 13);

            mockupData.Add(day1);
            mockupData.Add(day2);
            mockupData.Add(day3);

            AgendaTimeline.AgendaDays = mockupData;
        }
    }
}
