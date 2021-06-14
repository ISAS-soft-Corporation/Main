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
using System.Windows.Shapes;

namespace CinemaLive
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public Catalog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Ratio_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string msg = String.Format("Current value: {0}", e.NewValue);
        }

        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            int NumberList = int.Parse(List.Text);
            NumberList++;
            this.List.Text = Convert.ToString(NumberList);
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            int NumberList = int.Parse(List.Text);
            if (NumberList != 1) NumberList--;
            this.List.Text = Convert.ToString(NumberList);
        }

        private void Button_Favorite1_Click(object sender, RoutedEventArgs e)
        {
            Favorite1.Foreground = Brushes.Red;
        }

        private void Button_Favorite2_Click(object sender, RoutedEventArgs e)
        {
            Favorite2.Foreground = Brushes.Red;
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            Country.SelectedIndex = -1;
            Genres.SelectedIndex = -1;
            Year.Text = "";
            Ratio.Value = -1;
        }
    }
}
