using DocsControl.Model;
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
using System.Windows.Threading;
using static DocsControl.Model.Modules;

namespace DocsControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string user;
 

        public MainWindow(string user)
        {
            InitializeComponent();
            
            var users = new User
            {
                NickName = user
            };
            sp1.Children.Clear();
            sp1.Children.Add(new ViewModel.vmDashboard(user));

            lblUser.Content = user.Split('|')[1].ToUpper();
            this.user = user;
            
            DispatcherTimer dispatcherTimer = new DispatcherTimer();

            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var dateNow = DateTime.Now.ToString("F");

            lblDateTime.Content = dateNow;
        }

        private void buttonLogout(object sender, RoutedEventArgs e)
        {
            if (showWarning("DO YOU WANT TO LOGOUT?").Equals(true)) 
            {
                var login = new Login();
                this.Close();
                login.ShowDialog();
            }
            else
                return;
        }
        private void buttonShowList(object sender, RoutedEventArgs e)
        {
            var item = sender as TreeViewItem;        
            sp1.Children.Clear();
            sp1.Children.Add(new ViewModel.vmListPage(item.Header.ToString()));
        }
        private void navHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton.Equals(MouseButton.Left))
                DragMove();
        }
        private void buttonDashboard(object sender, RoutedEventArgs e)
        {
            sp1.Children.Clear();
            sp1.Children.Add(new ViewModel.vmDashboard(user));
           
        }
        private void buttonOutgoing(object sender, RoutedEventArgs e)
        {
            sp1.Children.Clear();
            sp1.Children.Add(new ViewModel.vmOutgoing(user));
        }

        private void buttonIncoming(object sender, RoutedEventArgs e)
        {
            sp1.Children.Clear();
            sp1.Children.Add(new ViewModel.vmIncoming(user));
        }
        private void buttonDocuments(object sender, RoutedEventArgs e)
        {
            sp1.Children.Clear();
            sp1.Children.Add(new ViewModel.vmDocuments());
        }
    }
}
