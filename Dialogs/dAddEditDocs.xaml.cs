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

namespace DocsControl.Dialogs
{
    /// <summary>
    /// Interaction logic for dAddEditDocs.xaml
    /// </summary>
    public partial class dAddEditDocs : Window
    {
        public dAddEditDocs()
        {
            InitializeComponent();
        }

        public dAddEditDocs(string dialogName)
        {
            InitializeComponent();
            lblTitle.Content = dialogName;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lblSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("saved!");
        }
    }
}
