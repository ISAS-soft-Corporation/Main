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
using System.Text.RegularExpressions;

namespace CinemaLive
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext mdb;
        public MainWindow()
        {
            InitializeComponent();
            shapka.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);


            mdb = new AppContext();
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Regist_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBox_Login.Text.Trim();
            string pass_input = PasswordBox_input.Password.Trim();
            string pass_confirm = PasswordBox_confirm.Password.Trim();
            string email = TextBox_Email.Text.Trim();
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool isItEmail = Regex.IsMatch(email, emailPattern);
            if (login.Length < 3 || login.Length > 12)
            {
                TextBox_Login.ToolTip = "Логин должен быть не меньше 5 символов и не больше 12";
                TextBox_Login.Background = Brushes.Firebrick;
            }
            else if (pass_input.Length < 5 || pass_input.Length > 15)
            {
                PasswordBox_input.ToolTip = "Пароль должен быть не меньше 5 символов и больше 15";
                PasswordBox_input.Background = Brushes.Firebrick;
            }
            else if (pass_input != pass_confirm)
            {
                PasswordBox_confirm.ToolTip = "Пароли не совпадают";
                PasswordBox_confirm.Background = Brushes.Firebrick;
            }
            else if (email.Length < 5 || email.Length > 150 || !isItEmail)
            {
                TextBox_Email.ToolTip = "Почта имеет неверный формат";
                TextBox_Email.Background = Brushes.Firebrick;
            }
            else
            {
                User userCheck = null;
                using (AppContext mdb = new AppContext())
                {
                    userCheck = mdb.Users.Where(s => s.Login == login).FirstOrDefault();
                }
                if (userCheck != null)
                {
                    TextBox_Login.ToolTip = "Логин занят";
                    TextBox_Login.Background = Brushes.Firebrick;
                }
                else
                {
                    TextBox_Login.ToolTip = "";
                    TextBox_Login.Background = Brushes.Transparent;
                    PasswordBox_input.ToolTip = "";
                    PasswordBox_input.Background = Brushes.Transparent;
                    PasswordBox_confirm.ToolTip = "";
                    PasswordBox_confirm.Background = Brushes.Transparent;
                    TextBox_Email.ToolTip = "";
                    TextBox_Email.Background = Brushes.Transparent;
                    Succesful.Visibility = Visibility.Visible;

                    User user = new User(login, pass_input, email);

                    mdb.Users.Add(user);
                    mdb.SaveChanges();

                    Message mess = new Message("Регистрация прошла успешно");
                    mess.ShowDialog();

                    Catalog catalog = new Catalog(user.Id, user.Login);
                    catalog.Show();
                    Hide();
                }
            }
        }

        private void Button_Entry_Click(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow();
            entryWindow.Show();
            Hide();
        }
    }
}
