using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для FilmCard.xaml
    /// </summary>
    public partial class FilmCard : Window
    {
        int user;
        string login;
        Movie movie;
        AppContext mdb;
        Genre genre;
        List<Casting> castings;
        List<Person> persons;
        public FilmCard(int user, string login, string filmName)
        {
            InitializeComponent();
            shapka.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
            this.user = user;
            this.login = login;
            Hello.Content += login;
            mdb = new AppContext();
            persons = new List<Person>();

            movie = mdb.Movies.Where(s => s.M_name == filmName).FirstOrDefault();
            genre = mdb.Genres.Where(s => s.GenreId == movie.M_g).FirstOrDefault();
            castings = mdb.Castings.Where(s => s.M_id == movie.MovieId).ToList();
            foreach (Casting casting in castings)
            {
                persons.Add(mdb.People.Where(s => s.PersonId == casting.P_id).FirstOrDefault());
            }
            outputInfo();
        }

        public void outputInfo()
        {
            Film_Name1.Text = movie.M_name;
            Film_Rate1.Text = movie.M_rating.ToString();

            System.Windows.Media.Imaging.BitmapImage bit =
                new BitmapImage(new Uri(movie.M_image, UriKind.Absolute));
            Poster.Source = bit;
            About.Text = movie.M_desc;
            Info1.Text = "Жанр: " + genre.G_name;
            Info1.Text += "\nРежиссер: ";
            Casting c_film = null;

            foreach (Casting c in castings)
            {
                if (c.P_role == "Режиссер")
                {
                    c_film = c;
                    break;
                }
            }
            if (c_film != null)
            {
                foreach (Person p in persons)
                {
                    if (c_film.P_id == p.PersonId)
                    {
                        Info1.Text += p.P_name;
                        break;
                    }
                }
            }

            About.Text += "\nАктеры:\n";
            string p_role = "";
            for (int i = 0; i < persons.Count(); i++)
            {
                foreach (Casting c in castings)
                {
                    if (c.P_id == persons[i].PersonId && c.P_role != "Режиссер" && c.M_id == movie.MovieId)
                    {
                        p_role = c.P_role;
                        break;
                    }
                }
                if(i == persons.Count - 1)
                {
                    About.Text += "актер: " + persons[i].P_name + ", роль: " + p_role;
                }
                else
                {
                    About.Text += "актер: " + persons[i].P_name + ", роль: " + p_role + "; ";
                }
            }

            WishList wishList1 = null;
            int m_id1 = movie.MovieId;
            wishList1 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id1).FirstOrDefault();
            if (wishList1 != null) Favorite.Foreground = Brushes.Red;
            else Favorite.Foreground = Brushes.Silver;
        }

        private void Button_Favorite_Click(object sender, RoutedEventArgs e)
        {
            WishList wishList = null;
            wishList = mdb.WishLists.Where(s => s.U_id == user && s.M_id == movie.MovieId).FirstOrDefault();
            if (wishList != null)
            {
                mdb.WishLists.Remove(wishList);
                mdb.SaveChanges();
                Favorite.Foreground = Brushes.Silver;
            }
            else
            {
                wishList = new WishList(user, movie.MovieId);
                mdb.WishLists.Add(wishList);
                mdb.SaveChanges();
                Favorite.Foreground = Brushes.Red;
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Back_Home_Click(object sender, RoutedEventArgs e)
        {
            
            Catalog catalog = new Catalog(user, login);
            catalog.Show();
            Close();
        }
        private void Button_Choise_Click(object sender, RoutedEventArgs e)
        {
            Choise choise = new Choise(user, login);
            choise.Show();
            Close();
        }

        private void Trailer(object sender, RoutedEventArgs e)
        {
            Process.Start(movie.M_trailer); 
        }
        private void Watch(object sender, RoutedEventArgs e)
        {
            Process.Start(movie.M_watch); 
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Confirm confirm = new Confirm();
            confirm.ShowDialog();
            ShowDialog();
        }
    }
}
