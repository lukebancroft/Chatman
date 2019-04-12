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
    public partial class Login : Window
    {
        MessagesViewModel mvm = new MessagesViewModel();

        public Login()
        {
            DataContext = mvm;
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            mvm.LoginPassword = LoginPassword.Password;
        }
    }
}
