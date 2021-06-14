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
            if (login.Length < 5)
            {
                TextBox_Login.ToolTip = "Поле введено не корректно";
                TextBox_Login.Background = Brushes.Firebrick;
            }
            else if (pass_input.Length < 5)
            {
                PasswordBox_input.ToolTip = "Поле введено не корректно";
                PasswordBox_input.Background = Brushes.Firebrick;
            }          
            else
            {
                TextBox_Login.ToolTip = "";
                TextBox_Login.Background = Brushes.Transparent;
                PasswordBox_input.ToolTip = "";
                PasswordBox_input.Background = Brushes.Transparent;
                Catalog catalog = new Catalog();
                catalog.Show();
                Hide();
            }
        }
    }
}
