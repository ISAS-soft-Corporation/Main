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
    /// Логика взаимодействия для EntryWindow.xaml
    /// </summary>
    public partial class EntryWindow : Window
    {
        public EntryWindow()
        {
            InitializeComponent();
            shapka.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Button_Entry_Click(object sender, RoutedEventArgs e)
        {

            string login = TextBox_Login.Text.Trim();
            string pass_input = PasswordBox_input.Password.Trim();

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
            else
            {
                TextBox_Login.ToolTip = "";
                TextBox_Login.Background = Brushes.Transparent;
                PasswordBox_input.ToolTip = "";
                PasswordBox_input.Background = Brushes.Transparent;

                User user = null;

                using(AppContext mdb = new AppContext())
                {
                    user = mdb.Users.Where(s=>s.Login == login && s.Password == pass_input).FirstOrDefault();
                }

                if (user != null)
                {
                    Message mess = new Message("Вход выполнен успешно");
                    mess.ShowDialog();
                    Catalog catalog = new Catalog(user.Id, user.Login);
                    catalog.Show();
                    Hide();
                }
                else
                {
                    Message mess = new Message("Проверьте введенные данные");
                    mess.ShowDialog();
                }
            }
        }
    }
}
