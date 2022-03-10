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
            //DocList();
            forSign();
            signed();
            released();
            lvForSign.SelectionChanged += LvForSign_SelectionChanged;
            lvSigned.SelectionChanged += LvForSign_SelectionChanged;
            lvForRelease.SelectionChanged += LvForSign_SelectionChanged;
        }        
        private void LvForSign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            var docData = (DocData)item.SelectedItem;
            this.Opacity = .5;
            this.Background = Brushes.Black;
            var dialog = new dAddEditDocs("EDIT DOCUMENT",docData.Id);
            dialog.ShowDialog();
            this.Opacity = 1;
            this.Background = Brushes.Transparent;
           
        }
        dbDocs db = new dbDocs();       
      
        private void forSign()
        {
            var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("FOR SIGNATURE")).ToList();
            var listDoc = new List<DocData>();
            foreach (var item in docs)
            {
                listDoc.Add(new DocData()
                {
                    Id = item.Id,
                    DocSubject = item.DocSubject,
                    ForSigned = item.ForSigned
                });                               
            }
            lvForSign.ItemsSource = listDoc;
        }
        private void signed()
        {
            var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("SIGNED")).ToList();
            var listDoc = new List<DocData>();
            foreach (var item in docs)
            {
                listDoc.Add(new DocData()
                {
                    Id = item.Id,
                    DocSubject = item.DocSubject,
                    Signed = item.Signed
                });
            }
            lvSigned.ItemsSource = listDoc;
        }
        private void released()
        {
            var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("RELEASED")).ToList();
            var listDoc = new List<DocData>();
            foreach (var item in docs)
            {
                listDoc.Add(new DocData()
                {
                    Id = item.Id,
                    DocSubject = item.DocSubject,
                    ForRelease = item.ForRelease
                });
            }
            lvForRelease.ItemsSource = listDoc;
        }

        private void buttonShowDialog(object sender, RoutedEventArgs e)
        {
            this.Opacity = .5;
            this.Background = Brushes.Black;            
            var dialog = new dAddEditDocs("ADD DOCUMENT",0);
            dialog.ShowDialog();
            this.Opacity = 1;
            this.Background = Brushes.Transparent;
            //lvForSign.SelectedIndex = -1;
            //openDialog(Window, new dAddEditDocs("ADD DOCUMENT"));
        }
    }
}
