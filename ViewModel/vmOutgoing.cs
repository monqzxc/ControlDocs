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
   
        public vmOutgoing(string user)
        {
            InitializeComponent();           
            this.DataContext = this;
            this.role = int.Parse(user.Split('|')[0]);
            this.nickName = user.Split('|')[1];
            this.user = user;

            if (role > 2)
            {
                btnAdd.Visibility = Visibility.Hidden;
            }
        }

        dbDocs db = new dbDocs();
        int role;
        string user;
        string nickName;
        private void lvMouseButton(object sender, MouseButtonEventArgs e) //event for clicking listview item
        {
            
            var item = sender as ListViewItem;
            {
                if (item != null && item.IsSelected)
                {
                    var docData = (DocData)item.Content;
                    this.Opacity = .5;
                    this.Background = Brushes.Black;
                    var dialog = new dAddEditDocs("EDIT OUTGOING DOCUMENT", docData.Id,user); //show edit form
                    dialog.ShowDialog();
                    this.Opacity = 1;
                    this.Background = Brushes.Transparent;
                }
            }
        }

        private void buttonShowDialog(object sender, RoutedEventArgs e) //event for clicking add button
        {
            if (role > 2)
                return;
            this.Opacity = .5;
            this.Background = Brushes.Black;
            var dialog = new dAddEditDocs("ADD OUTGOING DOCUMENT", 0,user);
            dialog.ShowDialog();
            this.Opacity = 1;
            this.Background = Brushes.Transparent;            
        }
        
        public ObservableCollection<DocData> ForSign //list of forSignature documents
        {
            get
            {
                var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("FOR SIGNATURE") && x.Tag.Equals("O")).ToList();
                var doctList = new ObservableCollection<DocData>();
                foreach (var item in docs.OrderByDescending(x => x.DateAdd))
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
        public ObservableCollection<DocData> Signed //list of signed documents
        {
            get
            {
                var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("SIGNED") && x.Tag.Equals("O")).ToList();
                var doctList = new ObservableCollection<DocData>();
                foreach (var item in docs.OrderByDescending(x => x.DateAdd))
                {
                    doctList.Add(new DocData()
                    {
                        Id = item.Id,
                        DocSubject = item.DocSubject,
                        Signed = item.Signed
                    });
                }
                return doctList;
            }
        }
        public ObservableCollection<DocData> Received //list of received documents
        {
            get
            {
                //subtracting of datessssss.

                var removeDate = DateTime.Now.AddDays(3);
                var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("RECEIVED") && x.Tag.Equals("O")).ToList();
                var doctList = new ObservableCollection<DocData>();
                foreach (var item in docs.OrderByDescending(x => x.DateAdd))
                {
                    if (DateTime.Now.Subtract(item.ForRelease.Value).TotalDays > 7)
                        continue;
                    //Console.WriteLine(DateTime.Now.Subtract(item.ForRelease.Value).TotalDays);
                    doctList.Add(new DocData()
                    {
                        Id = item.Id,
                        DocSubject = item.DocSubject,
                        ForRelease = item.ForRelease
                    });
                }
                return doctList;
            }           
        }       
    }
}
