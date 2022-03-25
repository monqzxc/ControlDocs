using DocsControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for dAddEditDocs2.xaml
    /// </summary>
    public partial class dAddEditDocs2 : Window
    {
        public dAddEditDocs2(string title, int id)
        {
            InitializeComponent();
            this.DataContext = this;

            //continuation of designing then finish the functionality
        }

        dbDocs db = new dbDocs();
        public ObservableCollection<NewCheckBox> CheckBoxes
        {
            get
            {
                var focal = db.Focals.ToList();
                var focalList = new ObservableCollection<NewCheckBox>();
                foreach (var item in focal)
                {
                    var chk = new NewCheckBox
                    {
                        Content = item.FullName,
                        Tag = item.Id.ToString(),                   
                    };
                    focalList.Add(chk);
                }
                return focalList;
            }
        }
        private void chkClickEvent(object sender, RoutedEventArgs e)
        {
            var item = sender as System.Windows.Controls.CheckBox;
            Console.WriteLine(item.Tag.ToString());
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton.Equals(MouseButton.Left))
                DragMove();
        }

    }
}
