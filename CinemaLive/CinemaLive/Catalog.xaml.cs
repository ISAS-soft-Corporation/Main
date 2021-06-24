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
            fillCountry();
            fillGenre();
            fillYear();
        }

        public void fillCountry()
        {

            foreach (Movie movie in movies) {

                if (Country.Items.IndexOf(movie.M_country) == -1)
                {
                    Country.Items.Add(movie.M_country);
                }
            }
        }

        public void fillYear()
        {
            List<int> years = new List<int>();
            foreach (Movie movie in movies)
            {
                if (years.IndexOf(movie.M_year) == -1)
                {
                    years.Add(movie.M_year);
                }
            }
            years.Sort();
            foreach (int year in years)
            {
                Year.Items.Add(year);
            }
        }

        public void fillGenre()
        {
            foreach (Genre genre in genres)
            {
                Genres.Items.Add(genre.G_name);
            }
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
                Info1.Text = "";
                Info2.Text = "";
                Film_Name1.Text = movies[index1].M_name;
                Film_Name2.Text = movies[index2].M_name;
                Film_Rate1.Text = movies[index1].M_rating.ToString();
                Film_Rate2.Text = movies[index2].M_rating.ToString();
                foreach (Genre g in genres)
                {
                    if (g.GenreId == movies[index1].M_g)
                        Info1.Text = "Жанр: " + g.G_name;
                    if (g.GenreId == movies[index2].M_g)
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
                    if (c.M_id == movies[index2].MovieId && c.P_role == "Режиссер")
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
                WishList wishList1 = null, wishList2 = null;
                int m_id1 = movies[index1].MovieId;
                int m_id2 = movies[index2].MovieId;
                wishList1 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id1).FirstOrDefault();
                wishList2 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id2).FirstOrDefault();
                if (wishList1 != null) Favorite1.Foreground = Brushes.Red;
                else Favorite1.Foreground = Brushes.Silver;
                if (wishList2 != null) Favorite2.Foreground = Brushes.Red;
                else Favorite2.Foreground = Brushes.Silver;
                VisibilityFilm2(true);
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
                WishList wishList1 = null;
                int m_id1 = movies[index1].MovieId;
                wishList1 = mdb.WishLists.Where(s => s.U_id == user && s.M_id == m_id1).FirstOrDefault();
                if (wishList1 != null) Favorite1.Foreground = Brushes.Red;
                else Favorite1.Foreground = Brushes.Silver;
                VisibilityFilm2(false);
            }
        }

        private void VisibilityFilm2(bool vis)
        {
            if (!vis)
            {
                Film2.Visibility = Visibility.Hidden;
                Info2.Visibility = Visibility.Hidden;
                SecondFilm.Visibility = Visibility.Hidden;
                Favorite2.Visibility = Visibility.Hidden;
                Film_Name2.Visibility = Visibility.Hidden;
                Film_Rate2.Visibility = Visibility.Hidden;
                rate2.Visibility = Visibility.Hidden;
            }
            else
            {
                Film2.Visibility = Visibility.Visible;
                Info2.Visibility = Visibility.Visible;
                SecondFilm.Visibility = Visibility.Visible;
                Favorite2.Visibility = Visibility.Visible;
                Film_Name2.Visibility = Visibility.Visible;
                Film_Rate2.Visibility = Visibility.Visible;
                rate2.Visibility = Visibility.Visible;
            }
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
            string msg = e.NewValue.ToString();
            Rating.Content = "Рейтинг: " + msg;
        }


        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            if (List.Text != "" && int.TryParse(List.Text, out int number))
            {
                int NumberList = int.Parse(List.Text);
                if (NumberList + 1 == pages)
                {
                    NumberList++;
                    this.List.Text = Convert.ToString(NumberList);
                    if (movies.Count % 2 != 0)
                    {
                        NumberList *= 2;
                        mainOutput(NumberList - 2, NumberList - 2);
                    }
                    else
                    {
                        NumberList *= 2;
                        mainOutput(NumberList - 2, NumberList - 1);
                    }
                }
                else if (NumberList != pages)
                {
                    NumberList++;
                    this.List.Text = Convert.ToString(NumberList);
                    NumberList *= 2;
                    mainOutput(NumberList - 2, NumberList - 1);
                }
            }
            else
            {
                this.List.Text = "1";
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            if (List.Text != "" && int.TryParse(List.Text, out int number))
            {
                int NumberList = int.Parse(List.Text);
                if (NumberList != 1)
                {
                    NumberList--;
                    this.List.Text = Convert.ToString(NumberList);
                    NumberList *= 2;
                    mainOutput(NumberList - 2, NumberList - 1);
                }
            }
            else
            {
                this.List.Text = "1";
            }

        }

        private void Button_Favorite1_Click(object sender, RoutedEventArgs e)
        {
            int m_id = -1;
            foreach (Movie movie in movies)
            {
                if(movie.M_name == Film_Name1.Text)
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

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            Country.SelectedIndex = -1;
            Genres.SelectedIndex = -1;
            Year.Text = "";
            Ratio.Value = -1;
            Search.Text = "";
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
            List.Text = "1";
            // Вывод первых двух фильмов
            mainOutput(0, 1);
        }

        private void Button_FirstFilm_Click(object sender, RoutedEventArgs e)
        {
            string film = Film_Name1.Text;
            FilmCard filmCard = new FilmCard(user, login, film);
            filmCard.Show();
            Close();

        }
        private void Button_SecondFilm_Click(object sender, RoutedEventArgs e)
        {
            string film = Film_Name2.Text;
            FilmCard filmCard = new FilmCard(user, login, film);
            filmCard.Show();
            Close();
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            Confirm confirm = new Confirm();
            confirm.Owner = this;
            confirm.ShowDialog();
        }

        private void Button_Choise_Click(object sender, RoutedEventArgs e)
        {
            Choise choise = new Choise(user, login);
            choise.Show();
            Close();
        }

        private void PageChanged(object sender, KeyEventArgs e)
        {
            if (List.Text != "")
            {
                if (int.TryParse(List.Text, out int number))
                {
                    int NumberList = int.Parse(List.Text);
                    if (NumberList >= 1 && NumberList < pages)
                    {
                        NumberList *= 2;
                        mainOutput(NumberList - 2, NumberList - 1);
                    }
                    else if (NumberList == pages)
                    {
                        if (movies.Count % 2 != 0)
                        {
                            NumberList *= 2;
                            mainOutput(NumberList - 2, NumberList - 2);
                        }
                        else
                        {
                            NumberList *= 2;
                            mainOutput(NumberList - 2, NumberList - 1);
                        }
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

        private int getGenreId(string genre)
        {
            int genreId = 0;
            foreach (Genre g in genres)
            {
                if (g.G_name == genre)
                {
                    genreId = g.GenreId;
                    break;
                }
            }
            return genreId;
        }

        private void SearchFilm(object sender, RoutedEventArgs e)
        {
            List<WishList> wishLists = null;
            if (checkFavourites.IsChecked == true)
            {
                wishLists = mdb.WishLists.Where(s => s.U_id == user).ToList();
                if (wishLists != null)
                {
                    movies = null;
                    movies = new List<Movie>();
                    foreach (WishList wishList in wishLists)
                    {
                        movies.Add(mdb.Movies.Where(s => s.MovieId == wishList.M_id).FirstOrDefault());
                    }
                }
                else
                {
                    Message mess = new Message("У вас нет фильмов в избранных. Рузультаты представлены из общего списка");
                    mess.ShowDialog();
                    checkFavourites.IsChecked = false;
                }
            }
            else if(movies.Count < 100) movies = mdb.Movies.ToList();
            string film = Search.Text;
            string country;
            if (Country.Text != null) country = Country.Text;
            else country = "";
            string genre;
            if (Genres.Text != null) genre = Genres.Text;
            else genre = "";
            int year;
            if (Year.Text != "")
            {
                year = int.Parse(Year.Text);
            }
            else
            {
                year = 0;
            }
            float rate = float.Parse(Ratio.Value.ToString());
            
            List<Movie> moviesSearch = null;

            // Все фильтры
            if (film != null && film != "" && year != 0 && genre != null && genre != "" 
                && rate != 0 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre); 
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_rating >= rate && movie.M_year == year
                        && movie.M_g == idGenre && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, жанр, страна, год
            else if (film != null && film != "" && year != 0 && genre != null && genre != ""
                && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_year == year
                        && movie.M_g == idGenre && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, жанр, страна, рейтинг
            else if (film != null && film != "" && genre != null && genre != ""
                && rate != 0 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_rating >= rate
                        && movie.M_g == idGenre && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, страна, рейтинг, год 
            else if (film != null && film != "" && year != 0 
                && rate != 0 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_rating >= rate && movie.M_year == year
                         && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Жанр, страна, рейтинг, год 
            else if (year != 0 && genre != null && genre != ""
                && rate != 0 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_rating >= rate && movie.M_year == year
                        && movie.M_g == idGenre && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, жанр, рейтинг, год 
            else if (film != null && film != "" && year != 0 && genre != null && genre != ""
                && rate != 0)
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_rating >= rate && movie.M_year == year
                        && movie.M_g == idGenre)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, жанр, страна,
            else if (film != null && film != "" && genre != null && genre != ""
                 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower())
                        && movie.M_g == idGenre && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, жанр, год
            else if (film != null && film != "" && year != 0 && genre != null && genre != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_year == year && movie.M_g == idGenre)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, жанр, рейтинг
            else if (film != null && film != "" && genre != null && genre != ""
                && rate != 0 )
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_rating >= rate 
                        && movie.M_g == idGenre)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, страна, рейтинг
            else if (film != null && film != "" 
                && rate != 0 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_rating >= rate && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, страна, год
            else if (film != null && film != "" && year != 0
                 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_name == film  && movie.M_year == year
                         && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, год, рейтинг
            else if (film != null && film != "" && year != 0
                && rate != 0)
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_rating >= rate && movie.M_year == year)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Жанр, страна, год
            else if ( year != 0 && genre != null && genre != ""
                 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_year == year
                        && movie.M_g == idGenre && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            //  Жанр, страна, рейтинг
            else if (genre != null && genre != ""
                && rate != 0 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if ( movie.M_rating >= rate 
                        && movie.M_g == idGenre && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Жанр, год, рейтинг
            else if (year != 0 && genre != null && genre != ""
                && rate != 0)
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_rating >= rate && movie.M_year == year
                        && movie.M_g == idGenre)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Страна, год, рейтинг
            else if (year != 0
                && rate != 0 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_rating >= rate && movie.M_year == year
                        && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, жанр
            else if (film != null && film != "" && genre != null && genre != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_g == idGenre)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, страна
            else if (film != null && film != "" && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, год
            else if (film != null && film != "" && year != 0)
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_year == year)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название, рейтинг
            else if (film != null && film != "" 
                && rate != 0)
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()) && movie.M_rating >= rate)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Жанр, страна
            else if ( genre != null && genre != "" && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_g == idGenre && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Жанр, год
            else if (year != 0 && genre != null && genre != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_year == year
                        && movie.M_g == idGenre)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Жанр, рейтинг
            else if ( genre != null && genre != "" && rate != 0)
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_rating >= rate && movie.M_g == idGenre)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Страна, рейтинг
            else if (rate != 0 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_rating >= rate  && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Страна, год
            else if (year != 0 && country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_year == year && movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Год, рейтинг 
            else if ( year != 0 && rate != 0)
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_rating >= rate && movie.M_year == year)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Название 
            else if (film != null && film != "")
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_name.ToLower().Contains(film.ToLower()))
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Жанр
            else if (genre != null && genre != "")
            {
                moviesSearch = new List<Movie>();
                int idGenre = getGenreId(genre);
                foreach (Movie movie in movies)
                {
                    if (movie.M_g == idGenre )
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Страна
            else if ( country != null && country != "")
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_country == country)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Год
            else if ( year != 0 )
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_year == year)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            // Рейтинг
            else if (rate != 0)
            {
                moviesSearch = new List<Movie>();
                foreach (Movie movie in movies)
                {
                    if (movie.M_rating >= rate)
                    {
                        moviesSearch.Add(movie);
                    }
                }
            }
            else
            {
                Message mess = new Message("Заполните хотя бы одно поле для поиска");
                mess.ShowDialog();
            }
        
            if (moviesSearch != null && moviesSearch.Count > 0)
            {
                movies = moviesSearch;
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
                // Вывод первых фильмов
                if (movies.Count > 1)
                {
                    mainOutput(0, 1);
                }
                else
                {
                    mainOutput(0, 0);
                }
                List.Text = "1";
            }
            else
            {
                Message mess = new Message("По вашему запросу фильмов не найдено(");
                mess.ShowDialog();
            }
        }

        private void checkFavourites_Checked(object sender, RoutedEventArgs e)
        {
            List<WishList> wishLists = null;
            wishLists = mdb.WishLists.Where(s => s.U_id == user).ToList();
            if (wishLists != null && wishLists.Count > 0)
            {
                movies = null;
                movies = new List<Movie>();
                foreach (WishList wishList in wishLists)
                {
                    movies.Add(mdb.Movies.Where(s => s.MovieId == wishList.M_id).FirstOrDefault());
                }
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
                List.Text = "1";
                // Вывод первых фильмов
                if (movies.Count > 1)
                {
                    mainOutput(0, 1);
                }
                else
                {
                    mainOutput(0, 0);
                }
            }
            else
            {
                Message mess = new Message("У вас нет фильмов в избранных. Рузультаты представлены из общего списка");
                mess.ShowDialog();
                checkFavourites.IsChecked = false;
            }
        }

        private void checkFavourites_Unchecked(object sender, RoutedEventArgs e)
        {
            movies = mdb.Movies.ToList();
            Country.SelectedIndex = -1;
            Genres.SelectedIndex = -1;
            Year.Text = "";
            Ratio.Value = -1;
            Search.Text = "";
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
            List.Text = "1";
            // Вывод первых двух фильмов
            mainOutput(0, 1);
        }
    }
}
