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
    /// Логика взаимодействия для Confirm.xaml
    /// </summary>
    public partial class Confirm : Window
    {
        public Confirm()
        {
            InitializeComponent();
            shapka.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click_Yes(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow();
            entryWindow.Show();
            Hide();
            this.Owner.Close();
        }
    }
}
