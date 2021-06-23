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
        AppContext mdb;
        List<Genre> genres;
        List<Movie> movies;
        List<Casting> castings;
        List<Person> persons;
        int pages;

        public Catalog(int user, string login)
        {
            InitializeComponent();
            shapka.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
            List.LostFocus += new RoutedEventHandler(List_LostFocus);
            this.user = user;
            this.login = login;

            Hello.Content += login;

            mdb = new AppContext();

            genres = mdb.Genres.ToList();
            movies = mdb.Movies.ToList();
            castings = mdb.Castings.ToList();
            persons = mdb.People.ToList();

            int all_movs = movies.Count();
            if (all_movs % 2 == 0)
            {
                pages = movies.Count() / 2;
            }
            else
            {
                pages = movies.Count() / 2 + 1;
            }
            MaterialDesignThemes.Wpf.HintAssist.SetHint(List, "Стр.(/" + pages.ToString() + ")");
            // Вывод первых двух фильмов
            mainOutput(0, 1);


        }

        public void mainOutput(int index1, int index2)
        {
            if (index2 - index1 == 1)
            {
                System.Windows.Media.Imaging.BitmapImage bit =
                new BitmapImage(new Uri(movies[index1].M_image, UriKind.Absolute));
                Image1.Source = bit;
                bit = new BitmapImage(new Uri(movies[index2].M_image, UriKind.Absolute));
                Image2.Source = bit;

                Film_Name1.Text = movies[index1].M_name;
                Film_Name2.Text = movies[index2].M_name;
                Film_Rate1.Text = movies[index1].M_rating.ToString();
                Film_Rate2.Text = movies[index2].M_rating.ToString();
                foreach (Genre g in genres)
                {
                    if (g.GenreId == movies[index1].M_g)
                        Info1.Text = "Жанр: " + g.G_name;
                    else if (g.GenreId == movies[index2].M_g)
                        Info2.Text = "Жанр: " + g.G_name;
                }
                Info1.Text += "\nРежиссер: ";
                Info2.Text += "\nРежиссер: ";
                Casting c_film1 = null, c_film2 = null;
                // Поиск режиссера
                foreach (Casting c in castings)
                {
                    if (c.M_id == movies[index1].MovieId && c.P_role == "Режиссер")
                    {
                        c_film1 = c;
                    }
                    else if (c.M_id == movies[index2].MovieId && c.P_role == "Режиссер")
                    {
                        c_film2 = c;
                    }
                }
                if (c_film1 != null && c_film2 != null)
                {
                    foreach (Person p in persons)
                    {
                        if (c_film1.P_id == p.PersonId)
                            Info1.Text += p.P_name;
                        else if (c_film2.P_id == p.PersonId)
                            Info2.Text += p.P_name;
                    }
                }
            }
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
            if (List.Text == "")
            {
                this.List.Text = "1";
            }
            int NumberList = int.Parse(List.Text);
            if (NumberList != pages)
            {
                NumberList++;
                this.List.Text = Convert.ToString(NumberList);
                NumberList *= 2;
                mainOutput(NumberList - 2, NumberList - 1);
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            if (List.Text == "")
            {
                this.List.Text = "1";
            }
            int NumberList = int.Parse(List.Text);
            if (NumberList != 1)
            {
                NumberList--;
                this.List.Text = Convert.ToString(NumberList);
                NumberList *= 2;
                mainOutput(NumberList - 2, NumberList - 1);
            }

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

        private void PageChanged(object sender, KeyEventArgs e)
        {
            if (List.Text != "")
            {
                int NumberList = int.Parse(List.Text);
                if (NumberList >= 1 && NumberList <= pages)
                {
                    NumberList *= 2;
                    mainOutput(NumberList - 2, NumberList - 1);
                }
                else
                {
                    this.List.Text = "1";
                }
            }
        }

        private void List_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = false;
            }
            else if (e.Key == Key.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void List_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (List.Text != "")
            {
                int NumberList = int.Parse(List.Text);
                if (NumberList * 10 + int.Parse(e.Text) > pages)
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (e.Text == "0")
                {
                    e.Handled = true;
                }
            }
        }
        private void List_LostFocus(object sender, RoutedEventArgs e)
        {
            if (List.Text == "")
            {
                this.List.Text = "1";
            }
        }
    }
}
