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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MultiwindowTestApp.Controls
{
    public sealed partial class AgendaTimelineView : Grid
    {
        public AgendaTimelineView()
        {
            this.InitializeComponent();
            CreateTimeRows();
        }

        private void CreateTimeRows()
        {
            Brush borderBrush = new SolidColorBrush(Colors.Gray);
            for(int i = 0; i < 96;  i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Star);

                TimeGrid.RowDefinitions.Add(rowDefinition);

                Border separatorLine = new Border();
                separatorLine.BorderThickness = new Thickness(0, 0, 0, 1);
                separatorLine.BorderBrush = borderBrush;

                TimeGrid.Children.Add(separatorLine);
                Grid.SetRow(separatorLine, i);
                Grid.SetColumnSpan(separatorLine, 2);
            }

        }
    }
}
