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

namespace Chatman
{
    /// <summary>
    /// Logique d'interaction pour AddUser.xaml
    /// </summary>
    public partial class Register : Window
    {
        MessagesViewModel mvm;

        public Register(MessagesViewModel messagesViewModel)
        {
            DataContext = messagesViewModel;
            mvm = messagesViewModel;
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            mvm.RegisterPassword = RegisterPassword.Password;
        }

        private void OnConfirmPasswordChanged(object sender, RoutedEventArgs e)
        {
            mvm.RegisterConfirmPassword = RegisterConfirmPassword.Password;
        }
    }
}
