using DocsControl.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DocsControl.ViewModel
{
    public partial class vmIncoming
    {
        
        public vmIncoming()
        {
            InitializeComponent();
        }

        private void buttonShowDialog(object sender, RoutedEventArgs e)
        {
            this.Opacity = .5;
            this.Background = Brushes.Black;
            var dialog = new dAddEditDocs2("ADD DOCUMENT", 0);
            dialog.ShowDialog();
            this.Opacity = 1;
            this.Background = Brushes.Transparent;
        }
    }
}
