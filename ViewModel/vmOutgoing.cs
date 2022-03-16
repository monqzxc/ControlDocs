using DocsControl.Dialogs;
using DocsControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class vmOutgoing 
    {
   
        public vmOutgoing()
        {
            InitializeComponent();           
            this.DataContext = this;
        }
        //public event PropertyChangedEventHandler PropertyChanged;

        //private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        DocDataList DocDataList = new DocDataList();
        public ObservableCollection<DocData> forSignature
        {
            get { return DocDataList.newList; }
        }
        
        private void lvMouseButton(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            {
                if (item != null && item.IsSelected)
                {
                    var docData = (DocData)item.Content;
                    this.Opacity = .5;
                    this.Background = Brushes.Black;
                    var dialog = new dAddEditDocs("EDIT DOCUMENT", docData.Id);
                    dialog.ShowDialog();
                    this.Opacity = 1;
                    this.Background = Brushes.Transparent;
                }
            }
        }

        private void buttonShowDialog(object sender, RoutedEventArgs e)
        {
            this.Opacity = .5;
            this.Background = Brushes.Black;
            var dialog = new dAddEditDocs("ADD DOCUMENT", 0);
            dialog.ShowDialog();
            this.Opacity = 1;
            this.Background = Brushes.Transparent;            
        }

        dbDocs db = new dbDocs();
        public ObservableCollection<DocData> ForSign
        {
            get
            {
                var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("FOR SIGNATURE")).ToList();
                var doctList = new ObservableCollection<DocData>();
                foreach (var item in docs)
                {
                    doctList.Add(new DocData()
                    {
                        Id = item.Id,
                        DocSubject = item.DocSubject,
                        ForSigned = item.ForSigned
                    });                    
                }
                return doctList;
            }
        }

        public ObservableCollection<DocData> Signed
        {
            get
            {
                var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("SIGNED")).ToList();
                var doctList = new ObservableCollection<DocData>();
                foreach (var item in docs)
                {
                    doctList.Add(new DocData()
                    {
                        Id = item.Id,
                        DocSubject = item.DocSubject,
                        ForSigned = item.ForSigned
                    });
                }
                return doctList;
            }
        }

        public ObservableCollection<DocData> Released
        {
            get
            {
                var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("RELEASED")).ToList();
                var doctList = new ObservableCollection<DocData>();
                foreach (var item in docs)
                {
                    doctList.Add(new DocData()
                    {
                        Id = item.Id,
                        DocSubject = item.DocSubject,
                        ForSigned = item.ForSigned
                    });
                }
                return doctList;
            }           
        }

        //public ObservableCollection<DocData> _forSignature = new ObservableCollection<DocData>();
        //public ObservableCollection<DocData> forSignature { get { return new DocData().DocDataList; } }

        //private void signed()
        //{
        //    var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("SIGNED")).ToList();
        //    var listDoc = new ObservableCollection<DocData>();
        //    foreach (var item in docs)
        //    {
        //        listDoc.Add(new DocData()
        //        {
        //            Id = item.Id,
        //            DocSubject = item.DocSubject,
        //            Signed = item.Signed
        //        });
        //    }
        //    lvSigned.ItemsSource = listDoc;
        //}
        //private void released()
        //{
        //    var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("RELEASED")).ToList();
        //    var listDoc = new ObservableCollection<DocData>();
        //    foreach (var item in docs)
        //    {
        //        listDoc.Add(new DocData()
        //        {
        //            Id = item.Id,
        //            DocSubject = item.DocSubject,
        //            ForRelease = item.ForRelease
        //        });
        //    }
        //    lvForRelease.ItemsSource = listDoc;
        //}   
        
    }
}
