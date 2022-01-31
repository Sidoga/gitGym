using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gym
{
    public partial class MainWindow : Window
    {       
        public MainWindow()
        {
            InitializeComponent();
            
        }
        static public string connectionString = @"Data Source = .\SQLEXPRESSSS;Initial Catalog = qwert2; Integrated Security = True";
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle a = (Rectangle)sender;
            a.Fill = Brushes.Black;
            a.Opacity = 0.25;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle a = (Rectangle)sender;
            a.Fill = Brushes.Transparent;
            a.Opacity = 0.1;
        }
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = Visibility.Hidden;
            frame.Visibility = Visibility.Visible;
            Rectangle a = (Rectangle)sender;
            if (a.Name == "Client")
            {
                frame.Source = new Uri($"{"Page1"}.xaml", UriKind.Relative);
            }
            else
            {
                frame.Source = new Uri($"{"Page2"}.xaml", UriKind.Relative);
            }
        }
    }
}
