using MultiwindowTestApp.Models;
using MultiwindowTestApp.Utilitys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MultiwindowTestApp.Controls
{
    public sealed partial class AgendaTimelineView : Grid
    {
        private Brush filledBorderBrush;
        private Brush dashedBorderBrush;
        private Brush dottedBorderBrush;
        private Brush timeStampBrush;

        private List<AgendaDay> agendaDays;
        public List<AgendaDay> AgendaDays {
            get { return this.agendaDays; }
            set {
                this.agendaDays = value;
                FillView();
            }
        }

        public AgendaTimelineView()
        {
            this.InitializeComponent();

            filledBorderBrush = (Brush)Resources["SystemControlForegroundBaseLowBrush"];
            dashedBorderBrush = (Brush)TimeGrid.Resources["HorizontalDashedBrush"];
            dottedBorderBrush = (Brush)TimeGrid.Resources["HorizontalDottedBrush"];
            timeStampBrush = (Brush)Resources["ButtonDisabledForegroundThemeBrush"];

            CreateTimeRows();
        }

        private void CreateTimeRows()
        {
            Thickness timeStampMargin = new Thickness(0, 0, 8, 0);

            for (int i = 0; i < 96;  i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Star);

                TimeGrid.RowDefinitions.Add(rowDefinition);

                Border separatorLine = new Border();

                TimeGrid.Children.Add(separatorLine);
                Grid.SetRow(separatorLine, i);
                Grid.SetColumn(separatorLine, 1);

                separatorLine.BorderThickness = new Thickness(0, 0, 0, 1);

                if(i % 4 == 3)
                {
                    // We do this if inside here, so the if-else loop gets in here and not to the ultimate esle if i=95
                    if(i != 95)
                    {
                        separatorLine.BorderBrush = filledBorderBrush;
                        Grid.SetColumn(separatorLine, 0);
                        Grid.SetColumnSpan(separatorLine, 2);
                    }
                } else if(i % 4 == 1)
                {
                    separatorLine.BorderBrush = dashedBorderBrush;
                } else
                {
                    separatorLine.BorderBrush = dottedBorderBrush;
                }

                if(i % 4 == 0)
                {
                    TextBlock timeStamp = new TextBlock();
                    timeStamp.Text = $"{((i +3) / 4)}";
                    timeStamp.FontSize = 20;
                    timeStamp.Foreground = timeStampBrush;
                    timeStamp.HorizontalAlignment = HorizontalAlignment.Right;
                    timeStamp.Margin = timeStampMargin;

                    TimeGrid.Children.Add(timeStamp);
                    Grid.SetRow(timeStamp, i);
                }
            }
        }

        private void AdjustContentGrid()
        {
            int numOfDays = AgendaDays.Count;

            if(ContentGrid.ColumnDefinitions.Count < numOfDays)
            {
                while(ContentGrid.ColumnDefinitions.Count < numOfDays)
                {
                    ColumnDefinition colDefinition = new ColumnDefinition();
                    colDefinition.Width = new GridLength(1, GridUnitType.Star);
                    ContentGrid.ColumnDefinitions.Add(colDefinition);
                }
            } else if(ContentGrid.ColumnDefinitions.Count > numOfDays)
            {
                while(ContentGrid.ColumnDefinitions.Count > numOfDays)
                {
                    ContentGrid.ColumnDefinitions.RemoveAt(ContentGrid.ColumnDefinitions.Count -1);
                }
            }
        }

        private void FillContentGrid()
        {

        }

        private void AdjustHeaderGrid(int numOfColumns)
        {
            if (ColumnDefinitions.Count < numOfColumns)
            {
                while (ColumnDefinitions.Count < numOfColumns)
                {
                    ColumnDefinition colDefinition = new ColumnDefinition();
                    colDefinition.Width = new GridLength(1, GridUnitType.Star);
                    ColumnDefinitions.Add(colDefinition);
                }
            }
            else if (ColumnDefinitions.Count > numOfColumns)
            {
                while (ColumnDefinitions.Count > numOfColumns)
                {
                    ColumnDefinitions.RemoveAt(ContentGrid.ColumnDefinitions.Count - 1);
                }
            }

            Grid.SetColumnSpan(ContentScroll, numOfColumns);
        }


        private void FillHeaderGrid(int numOfUsersShown)
        {
            int iDay = 1;

            foreach (AgendaDay day in agendaDays)
            {
                Border dayHeaderBack = new Border();
                dayHeaderBack.BorderBrush = filledBorderBrush;

                // special border for the last element
                if (day == agendaDays.Last())
                {
                    dayHeaderBack.BorderThickness = new Thickness(0, 0, 0, 1);
                }
                else
                {
                    dayHeaderBack.BorderThickness = new Thickness(0, 0, 2, 1);
                }

                TextBlock dayHeader = new TextBlock();
                dayHeader.Text = day.Date.ToString("d");
                dayHeader.HorizontalAlignment = HorizontalAlignment.Center;
                dayHeader.VerticalAlignment = VerticalAlignment.Top;
                dayHeader.Foreground = timeStampBrush;
                dayHeader.Margin = new Thickness(0, 4, 0, 0);

                // special indicator id the date is today
                if(day.Date == DateTime.Today)
                {
                    Rectangle todayIndicator = new Rectangle();
                    todayIndicator.Height = 4;
                    todayIndicator.Fill = (Brush)Resources["SystemControlHighlightAccentBrush"];
                    todayIndicator.VerticalAlignment = VerticalAlignment.Top;

                    StackPanel dayHeaderStack = new StackPanel();
                    dayHeaderStack.Orientation = Orientation.Vertical;

                    dayHeaderStack.Children.Add(todayIndicator);
                    dayHeaderStack.Children.Add(dayHeader);
                    dayHeader.Margin = new Thickness(0, 0, 0, 0);

                    dayHeaderBack.Child = dayHeaderStack;
                } else
                {
                    dayHeaderBack.Child = dayHeader;
                }

                Children.Add(dayHeaderBack);
                Grid.SetRow(dayHeaderBack, 0);
                Grid.SetColumn(dayHeaderBack, iDay);
                Grid.SetRowSpan(dayHeaderBack, 2);
                Grid.SetColumnSpan(dayHeaderBack, numOfUsersShown);

                int iUsr = 0;
                foreach(string user in day.AppointmentsPerUser.Keys)
                {
                    Border usrHeaderBack = new Border();
                    usrHeaderBack.BorderBrush = (Brush)Resources["SystemControlHighlightAccentBrush"];
                    usrHeaderBack.BorderThickness = new Thickness(0, 0, 0, 4);
                    

                    TextBlock usrHeader = new TextBlock();
                    usrHeader.Text = user;
                    usrHeader.HorizontalAlignment = HorizontalAlignment.Center;
                    usrHeader.Foreground = timeStampBrush;

                    usrHeaderBack.Child = usrHeader;
                    Children.Add(usrHeaderBack);
                    Grid.SetRow(usrHeaderBack, 1);
                    Grid.SetColumn(usrHeaderBack, iDay + iUsr);

                    iUsr++;
                }


                iDay = iDay + numOfUsersShown;
            }
        }

        private void FillView()
        {
            // breaking condition for the errorcase that we have no agendadays
            if (agendaDays.Count == 0)
            {
                return;
            }

            // If column count doesn't match the amount of days, adjust the grid
            if (ContentGrid.ColumnDefinitions.Count != agendaDays.Count)
            {
                AdjustContentGrid();
            }

            // calculate the amount of different header columns needed and if necessary, adjust the grid
            // For this operation we assume, that for every day the entrys for same ammount of users is shown
            int numOfUsersShown = agendaDays.First().AppointmentsPerUser.Count;
            int headerColumnsNeeded = agendaDays.Count * numOfUsersShown + 1;
            headerColumnsNeeded = headerColumnsNeeded < 2 ? 2 : headerColumnsNeeded;
            if (ColumnDefinitions.Count != headerColumnsNeeded)
            {
                AdjustHeaderGrid(headerColumnsNeeded);
            }

            FillHeaderGrid(numOfUsersShown);

            FillContentGrid();
        }
    }
}
