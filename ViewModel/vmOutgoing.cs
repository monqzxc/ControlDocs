using DocsControl.Dialogs;
using DocsControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static DocsControl.Model.DocData;
using static DocsControl.Model.Modules;

namespace DocsControl.ViewModel
{
    public partial class vmOutgoing : UserControl
    {        
        public vmOutgoing()
        {
            InitializeComponent();
            DocList();
            
            lvForSign.SelectionChanged += LvForSign_SelectionChanged;
            lvSigned.SelectionChanged += LvForSign_SelectionChanged;
            lvForRelease.SelectionChanged += LvForSign_SelectionChanged;

        }
        
        private void LvForSign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var item = (ListBox)sender;
            var office = (Office)item.SelectedItem;
            this.Opacity = .5;
            this.Background = Brushes.Black;
            var dialog = new dAddEditDocs("EDIT DOCUMENT");
            dialog.ShowDialog();
            this.Opacity = 1;
            this.Background = Brushes.Transparent;
        }

        public void DocList()
        {
            var db = new dbDocs();
            var offices = db.Offices.ToList();
            var officeList = new List<Office>();
            var sampleLilst = new string[] { 
                "1,Hatdog,For Sign,2022-10-29,-,-,Memo,1-,-",
                "2,DogHat,For Sign,2022-12-15,-,-,Letter,1-,-", 
                "3,GotHag,For Sign,2022-01-09,-,-,CTPR,1-,-", 
                "4,TogHad,For Sign,2022-07-31,-,-,Advisory,1-,-" };
            foreach (var item in offices)
            {
                officeList.Add(new Office() { OperatingUnit = item.OperatingUnit, Email = DateTime.Now.ToString("yyyy-MM-dd")} );
                lvForSign.ItemsSource = officeList;
                lvSigned.ItemsSource = officeList;
            }
        }
      

        private void buttonShowDialog(object sender, RoutedEventArgs e)
        {
            this.Opacity = .5;
            this.Background = Brushes.Black;            
            var dialog = new dAddEditDocs("ADD DOCUMENT");
            dialog.ShowDialog();
            this.Opacity = 1;
            this.Background = Brushes.Transparent;
            //openDialog(Window, new dAddEditDocs("ADD DOCUMENT"));
        }
    }
}
