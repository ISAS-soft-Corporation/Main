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
        int user;
        string login;
        
        
        public Catalog(int user, string login)
        {
            InitializeComponent();
            shapka.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
            this.user = user;
            this.login = login;

            Hello.Content += login;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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

        private void Button_FirstFilm_Click(object sender, RoutedEventArgs e)
        {
            FilmCard filmCard = new FilmCard(user, login);
            filmCard.Show();
            Hide();
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow();
            entryWindow.Show();
            Hide();
        }

        private void Button_Choise_Click(object sender, RoutedEventArgs e)
        {
            Choise choise = new Choise(user, login);
            choise.Show();
            Hide();
        }
    }
}
