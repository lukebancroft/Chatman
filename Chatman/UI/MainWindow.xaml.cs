using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Chatman
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(MessagesViewModel messagesViewModel)
        {
            DataContext = messagesViewModel;
            InitializeComponent();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser((MessagesViewModel)DataContext);
            addUser.Show();
        }
    }
}
