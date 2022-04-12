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
using DocsControl.Model;
using static DocsControl.Model.User;

namespace DocsControl
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            var btn = buttonLogin;
            txtPassword.KeyDown += (s, e) =>
            {
                if (e.Key.Equals(Key.Enter))
                    buttonClick(btn, new EventArgs());
            };
            txtUsername.KeyDown += (s, e) =>
            {
                if (e.Key.Equals(Key.Enter))
                    buttonClick(btn, new EventArgs());
            };
            pagination.TotalDocs = 150;
            this.DataContext = this;
        }
        public Pagination pagination = new Pagination();
        public List<NewButton> NewButtons
        {
            get { return pagination.NewButtons; }
        }
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton.Equals(MouseButton.Left))
                DragMove();
        }
       
        public void buttonClick(object o, EventArgs e)
        {

            var btn = o as Button;
            Cursor = Cursors.Wait;
            switch (btn.Content)
            {
                case "LOGIN":

                    var user = new User
                    {
                        UserName = txtUsername.Text,
                        Password = txtPassword.Password.ToString()
                    };
                    
                    if (user.GetNickname().Contains("Invalid"))
                    {
                        MessageBox.Show(user.GetNickname());
                        Cursor = Cursors.Arrow;
                        return;
                    }
                    var mainWindow = new MainWindow(user.GetNickname());
                    this.Close();
                    mainWindow.Show();
                    
                    break;
                case "EXIT":                   
                    this.Close();
                    break;
            }
            Cursor = Cursors.Arrow;
        }
    }
}
