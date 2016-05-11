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
        public List<AgendaDay> AgendaDays {
            get { return AgendaDays; }
            set {
                AgendaDays = value;
                FillContentGrid();
            }
        }

        public AgendaTimelineView()
        {
            this.InitializeComponent();
            CreateTimeRows();
        }

        private void CreateTimeRows()
        {
            
            Brush filledBorderBrush = (Brush)Resources["SystemControlForegroundBaseLowBrush"];
            Brush dashedBorderBrush = (Brush)TimeGrid.Resources["HorizontalDashedBrush"];
            Brush dottedBorderBrush = (Brush)TimeGrid.Resources["HorizontalDottedBrush"];
            Brush timeStampBrush = (Brush)Resources["ButtonDisabledForegroundThemeBrush"];
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

        private void AdjustHeaderGrid()
        {

        }

        private void FillContentGrid()
        {
            // breaking condition for the errorcase that we have no agendadays
            if (AgendaDays.Count == 0)
            {
                return;
            }

            // If column count doesn't match the amount of days, adjust the grid
            if (ContentGrid.ColumnDefinitions.Count != AgendaDays.Count)
            {
                AdjustContentGrid();
            }

            // calculate the amount of different header columns needed
            AgendaDays.Count * AgendaDays.First().AppointmentsPerUser.Count();
            if(AgendaDays.Count > 0)
            {

            }
            if(ColumnDefinitions.Count != 0)
            {

            }


        }
    }
}
