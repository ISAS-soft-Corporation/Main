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
    /// Логика взаимодействия для Choise.xaml
    /// </summary>
    public partial class Choise : Window
    {
        int user;
        string login;
        AppContext mdb;
        List<Genre> genres;
        List<Movie> movies;
        List<Casting> castings;
        List<Person> persons;
        List<WishList> wishLists;
        int pages;

        public Choise(int user, string login)
        {
            InitializeComponent();
            shapka.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
            this.user = user;
            this.login = login;
            mdb = new AppContext();
            persons = new List<Person>();
            movies = new List<Movie>();
            castings = new List<Casting>();
            genres = new List<Genre>();

            wishLists = mdb.WishLists.Where(s => s.U_id == user).ToList();
            if (wishLists != null && wishLists.Count() > 0)
            {
                foreach (WishList wishList in wishLists)
                {
                    movies.Add(mdb.Movies.Where(s => s.MovieId == wishList.M_id).FirstOrDefault());
                }

                foreach (Movie movie in movies)
                {
                    castings.Add(mdb.Castings.Where(s => s.M_id == movie.MovieId).FirstOrDefault());
                    genres.Add(mdb.Genres.Where(s => s.GenreId == movie.M_g).FirstOrDefault());
                }

                foreach (Casting casting in castings)
                {
                    persons.Add(mdb.People.Where(s => s.PersonId == casting.P_id).FirstOrDefault());
                }

                Hello.Content += login;
                int all_movs = movies.Count();
                if (all_movs % 3 == 0)
                {
                    pages = movies.Count() / 3;
                }
                else
                {
                    pages = movies.Count() / 3 + 1;
                }
                MaterialDesignThemes.Wpf.HintAssist.SetHint(List, "Стр.(/" + pages.ToString() + ")");
                // Вывод первых фильмов
                if (pages == 1 && all_movs % 3 != 0){
                    if (all_movs % 3 == 2) mainOutput(0, 1);
                    else if (all_movs % 3 == 1) mainOutput(0, 0);
                }
                else mainOutput(0, 2);
            }
            else
            {
                Films.Visibility = Visibility.Hidden;
                Navigation.Visibility = Visibility.Hidden;
                NoFilms.Visibility = Visibility.Visible;
            }
        }

        private void VisibilityFilms23(int vis)
        {
            // 1 - прятаем фильм 2
            // 2 - прятаем фильм 3
            // 3 - показываем фильм 2
            // 4 - показываем фильм 3
            if (vis == 1)
            {
                Film2.Visibility = Visibility.Hidden;
                Info2.Visibility = Visibility.Hidden;
                SecondFilm.Visibility = Visibility.Hidden;
                Favorite2.Visibility = Visibility.Hidden;
                Film_Name2.Visibility = Visibility.Hidden;
                Film_Rate2.Visibility = Visibility.Hidden;
                rate2.Visibility = Visibility.Hidden;
            }
            else if (vis == 2)
            {
                Film3.Visibility = Visibility.Hidden;
                Info3.Visibility = Visibility.Hidden;
                ThirdFilm.Visibility = Visibility.Hidden;
                Favorite3.Visibility = Visibility.Hidden;
                Film_Name3.Visibility = Visibility.Hidden;
                Film_Rate3.Visibility = Visibility.Hidden;
                rate3.Visibility = Visibility.Hidden;
            }
            else if (vis == 3)
            {
                Film2.Visibility = Visibility.Visible;
                Info2.Visibility = Visibility.Visible;
                SecondFilm.Visibility = Visibility.Visible;
                Favorite2.Visibility = Visibility.Visible;
                Film_Name2.Visibility = Visibility.Visible;
                Film_Rate2.Visibility = Visibility.Visible;
                rate2.Visibility = Visibility.Visible;
            }
            else
            {
                Film3.Visibility = Visibility.Visible;
                Info3.Visibility = Visibility.Visible;
                ThirdFilm.Visibility = Visibility.Visible;
                Favorite3.Visibility = Visibility.Visible;
                Film_Name3.Visibility = Visibility.Visible;
                Film_Rate3.Visibility = Visibility.Visible;
                rate3.Visibility = Visibility.Visible;
            }
        }

        public void mainOutput(int index1, int index3)
        {
            if (index3 - index1 == 2)
            {
                System.Windows.Media.Imaging.BitmapImage bit =
                new BitmapImage(new Uri(movies[index1].M_image, UriKind.Absolute));
                Image1.Source = bit;
                bit = new BitmapImage(new Uri(movies[index3 - 1].M_image, UriKind.Absolute));
                Image2.Source = bit;
                bit = new BitmapImage(new Uri(movies[index3].M_image, UriKind.Absolute));
                Image3.Source = bit;
                Info1.Text = "";
                Info2.Text = "";
                Film_Name1.Text = movies[index1].M_name;
                Film_Name2.Text = movies[index3-1].M_name;
                Film_Name3.Text = movies[index3].M_name;
                Film_Rate1.Text = movies[index1].M_rating.ToString();
                Film_Rate2.Text = movies[index3-1].M_rating.ToString();
                Film_Rate3.Text = movies[index3].M_rating.ToString();
                foreach (Genre g in genres)
                {
                    if (g.GenreId == movies[index1].M_g)
                        Info1.Text = "Жанр: " + g.G_name;
                    if (g.GenreId == movies[index3-1].M_g)
                        Info2.Text = "Жанр: " + g.G_name;
                    if (g.GenreId == movies[index3].M_g)
                        Info3.Text = "Жанр: " + g.G_name;
                }
                Info1.Text += "\nРежиссер: ";
                Info2.Text += "\nРежиссер: ";
                Info3.Text += "\nРежиссер: ";
                Casting c_film1 = null, c_film2 = null, c_film3 = null;
                // Поиск режиссера
                foreach (Casting c in castings)
                {
                    if (c.M_id == movies[index1].MovieId && c.P_role == "Режиссер")
                    {
                        c_film1 = c;
                    }
                    else if (c.M_id == movies[index3-1].MovieId && c.P_role == "Режиссер")
                    {
                        c_film2 = c;
                    }
                    else if (c.M_id == movies[index3].MovieId && c.P_role == "Режиссер")
                    {
                        c_film3 = c;
                    }
                }
                if (c_film1 != null && c_film2 != null && c_film3 != null)
                {
                    foreach (Person p in persons)
                    {
                        if (c_film1.P_id == p.PersonId)
                            Info1.Text += p.P_name;
                        else if (c_film2.P_id == p.PersonId)
                            Info2.Text += p.P_name;
                        else if (c_film3.P_id == p.PersonId)
                            Info3.Text += p.P_name;
                    }
                }
                string desc1 = movies[index1].M_desc;
                string desc2 = movies[index3-1].M_desc;
                string desc3 = movies[index3].M_desc;
                Info1.Text += "\n" + desc1.Remove(70, desc1.Length - 71) + "...";
                Info2.Text += "\n" + desc2.Remove(70, desc2.Length - 71) + "...";
                Info3.Text += "\n" + desc2.Remove(70, desc3.Length - 71) + "...";
                WishList wishList1 = null, wishList2 = null, wishList3 = null;
                int m_id1 = movies[index1].MovieId;
                int m_id2 = movies[index3-1].MovieId;
                int m_id3 = movies[index3].MovieId;
                wishList1 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id1).FirstOrDefault();
                wishList2 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id2).FirstOrDefault();
                wishList3 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id3).FirstOrDefault();
                if (wishList1 != null) Favorite1.Foreground = Brushes.Red;
                else Favorite1.Foreground = Brushes.Silver;
                if (wishList2 != null) Favorite2.Foreground = Brushes.Red;
                else Favorite2.Foreground = Brushes.Silver;
                if (wishList3 != null) Favorite3.Foreground = Brushes.Red;
                else Favorite3.Foreground = Brushes.Silver;
                VisibilityFilms23(3); // 3 - показываем фильм 2                                     
                VisibilityFilms23(4); // 4 - показываем фильм 3
            }
            else if (index3 - index1 == 2)
            {
                System.Windows.Media.Imaging.BitmapImage bit =
                new BitmapImage(new Uri(movies[index1].M_image, UriKind.Absolute));
                Image1.Source = bit;
                bit = new BitmapImage(new Uri(movies[index3].M_image, UriKind.Absolute));
                Image2.Source = bit;
                Info1.Text = "";
                Info2.Text = "";
                Film_Name1.Text = movies[index1].M_name;
                Film_Name2.Text = movies[index3].M_name;
                Film_Rate1.Text = movies[index1].M_rating.ToString();
                Film_Rate2.Text = movies[index3].M_rating.ToString();
                foreach (Genre g in genres)
                {
                    if (g.GenreId == movies[index1].M_g)
                        Info1.Text = "Жанр: " + g.G_name;
                    if (g.GenreId == movies[index3].M_g)
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
                    else if (c.M_id == movies[index3].MovieId && c.P_role == "Режиссер")
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
                string desc1 = movies[index1].M_desc;
                string desc2 = movies[index3].M_desc;
                Info1.Text += "\n" + desc1.Remove(70, desc1.Length - 71) + "...";
                Info2.Text += "\n" + desc2.Remove(70, desc2.Length - 71) + "...";
                WishList wishList1 = null, wishList2 = null;
                int m_id1 = movies[index1].MovieId;
                int m_id2 = movies[index3].MovieId;
                wishList1 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id1).FirstOrDefault();
                wishList2 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id2).FirstOrDefault();
                if (wishList1 != null) Favorite1.Foreground = Brushes.Red;
                else Favorite1.Foreground = Brushes.Silver;
                if (wishList2 != null) Favorite2.Foreground = Brushes.Red;
                else Favorite2.Foreground = Brushes.Silver;
                VisibilityFilms23(2); // 2 - прятаем фильм 3
                VisibilityFilms23(3); // 3 - показываем фильм 2
            }
            else
            {
                System.Windows.Media.Imaging.BitmapImage bit =
                new BitmapImage(new Uri(movies[index1].M_image, UriKind.Absolute));
                Image1.Source = bit;

                Film_Name1.Text = movies[index1].M_name;
                Film_Rate1.Text = movies[index1].M_rating.ToString();
                foreach (Genre g in genres)
                {
                    if (g.GenreId == movies[index1].M_g)
                        Info1.Text = "Жанр: " + g.G_name;
                }
                Info1.Text += "\nРежиссер: ";
                Casting c_film1 = null;
                // Поиск режиссера
                foreach (Casting c in castings)
                {
                    if (c.M_id == movies[index1].MovieId && c.P_role == "Режиссер")
                    {
                        c_film1 = c;
                    }
                }
                if (c_film1 != null)
                {
                    foreach (Person p in persons)
                    {
                        if (c_film1.P_id == p.PersonId)
                            Info1.Text += p.P_name;
                    }
                }
                string desc1 = movies[index1].M_desc;
                Info1.Text += "\n" + desc1.Remove(70, desc1.Length - 71) + "...";
                WishList wishList1 = null;
                int m_id1 = movies[index1].MovieId;
                wishList1 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id1).FirstOrDefault();
                if (wishList1 != null) Favorite1.Foreground = Brushes.Red;
                else Favorite1.Foreground = Brushes.Silver;
                VisibilityFilms23(1); // 1 - прятаем фильм 2
                VisibilityFilms23(2); // 2 - прятаем фильм 3
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
        private void Button_Favorite1_Click(object sender, RoutedEventArgs e)
        {
            int m_id = -1;
            foreach (Movie movie in movies)
            {
                if (movie.M_name == Film_Name1.Text)
                {
                    m_id = movie.MovieId;
                    break;
                }
            }
            WishList wishList = null;
            wishList = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id).FirstOrDefault();
            if (wishList != null)
            {
                mdb.WishLists.Remove(wishList);
                mdb.SaveChanges();
                Favorite1.Foreground = Brushes.Silver;
            }
            else
            {
                wishList = new WishList(user, m_id);
                mdb.WishLists.Add(wishList);
                mdb.SaveChanges();
                Favorite1.Foreground = Brushes.Red;
            }

        }
        private void Button_Favorite2_Click(object sender, RoutedEventArgs e)
        {
            int m_id = -1;
            foreach (Movie movie in movies)
            {
                if (movie.M_name == Film_Name2.Text)
                {
                    m_id = movie.MovieId;
                    break;
                }
            }
            WishList wishList = null;
            wishList = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id).FirstOrDefault();
            if (wishList != null)
            {
                mdb.WishLists.Remove(wishList);
                mdb.SaveChanges();
                Favorite2.Foreground = Brushes.Silver;
            }
            else
            {
                wishList = new WishList(user, m_id);
                mdb.WishLists.Add(wishList);
                mdb.SaveChanges();
                Favorite2.Foreground = Brushes.Red;
            }
        }

        private void Button_Favorite3_Click(object sender, RoutedEventArgs e)
        {
            int m_id = -1;
            foreach (Movie movie in movies)
            {
                if (movie.M_name == Film_Name3.Text)
                {
                    m_id = movie.MovieId;
                    break;
                }
            }
            WishList wishList = null;
            wishList = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id).FirstOrDefault();
            if (wishList != null)
            {
                mdb.WishLists.Remove(wishList);
                mdb.SaveChanges();
                Favorite3.Foreground = Brushes.Silver;
            }
            else
            {
                wishList = new WishList(user, m_id);
                mdb.WishLists.Add(wishList);
                mdb.SaveChanges();
                Favorite3.Foreground = Brushes.Red;
            }
        }

        private void Button_FirstFilm_Click(object sender, RoutedEventArgs e)
        {
            string film = Film_Name1.Text;
            FilmCard filmCard = new FilmCard(user, login, film);
            filmCard.Show();
            Hide();

        }
        private void Button_SecondFilm_Click(object sender, RoutedEventArgs e)
        {
            string film = Film_Name2.Text;
            FilmCard filmCard = new FilmCard(user, login, film);
            filmCard.Show();
            Hide();
        }
        private void Button_ThirdFilm_Click(object sender, RoutedEventArgs e)
        {
            string film = Film_Name3.Text;
            FilmCard filmCard = new FilmCard(user, login, film);
            filmCard.Show();
            Hide();
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow();
            entryWindow.Show();
            Hide();
        }
        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            if (List.Text == "")
            {
                this.List.Text = "1";
            }
            int NumberList = int.Parse(List.Text);
            if (NumberList + 1 == pages)
            {
                NumberList++;
                this.List.Text = Convert.ToString(NumberList);
                if (movies.Count % 3 == 2)
                {
                    NumberList *= 3;
                    mainOutput(NumberList - 3, NumberList - 2);
                }
                else if (movies.Count % 3 == 1)
                {
                    NumberList *= 3;
                    mainOutput(NumberList - 3, NumberList - 3);
                }
                else
                {
                    NumberList *= 3;
                    mainOutput(NumberList - 3, NumberList - 1);
                }
            }
            else if (NumberList != pages)
            {
                NumberList++;
                this.List.Text = Convert.ToString(NumberList);
                NumberList *= 3;
                mainOutput(NumberList - 3, NumberList - 1);
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
                NumberList *= 3;
                mainOutput(NumberList - 3, NumberList - 1);
            }

        }

        private void Button_Theatres_Click(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog(user, login);
            catalog.Show();
            Hide();
        }

        private void PageChanged(object sender, KeyEventArgs e)
        {
            if (List.Text != "")
            {
                int NumberList = int.Parse(List.Text);
                if (NumberList >= 1 && NumberList < pages)
                {
                    NumberList *= 3;
                    mainOutput(NumberList - 3, NumberList - 1);
                }
                else if (NumberList == pages)
                {
                    if (movies.Count % 3 == 2)
                    {
                        NumberList *= 3;
                        mainOutput(NumberList - 3, NumberList - 2);
                    }
                    else if (movies.Count % 3 == 1)
                    {
                        NumberList *= 3;
                        mainOutput(NumberList - 3, NumberList - 3);
                    }
                    else
                    {
                        NumberList *= 3;
                        mainOutput(NumberList - 3, NumberList - 1);
                    }
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
    }
}
