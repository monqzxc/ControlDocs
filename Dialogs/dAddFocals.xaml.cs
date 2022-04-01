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
using static DocsControl.Model.Modules;

namespace DocsControl.Dialogs
{
    /// <summary>
    /// Interaction logic for dAddFocals.xaml
    /// </summary>
    public partial class dAddFocals : Window
    {
        public dAddFocals(string user)
        {
            InitializeComponent();
            this.role = int.Parse(user.Split('|')[0]);
            this.nickname = user.Split('|')[1];
            this.user = user;
        }
        private string user;
        private string nickname;
        private int role;

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton.Equals(MouseButton.Left))
                DragMove();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (showWarning("DO YOU WANT TO CANCEL?").Equals(true))
            {
                //store the path in the list to be deleted/removed
                this.Close();
            }
            else
                return;
        }

        private bool isValid()
        {


            return true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
