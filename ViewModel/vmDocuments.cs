using DocsControl.Dialogs;
using DocsControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static DocsControl.Model.Modules;

namespace DocsControl.ViewModel
{
    public partial class vmDocuments : UserControl
    {
        public vmDocuments()
        {
            InitializeComponent();
            loadComboBox();
            loadDocList();
            this.DataContext = this;
        }

        dbDocs db = new dbDocs();

        private void loadComboBox()
        {
            var subject = db.DocDatas.Select(x => x.DocSubject).ToList();
            var docType = db.DocTypes.Select(x => x.DocumentType).ToList();
            var controlNumber = db.DocDatas.Select(x => x.DocControlNumber).ToList();
            
            ComboBox(cmbSubject, subject);
            ComboBox(cmbDocType, docType);
            ComboBox(cmbControlNumber, controlNumber);
    
        }

        private ObservableCollection<DocData> sampleDocs = new ObservableCollection<DocData>();
        public ObservableCollection<DocData> DocDatas
        {
            get { return sampleDocs; }
            set { sampleDocs = value; }
        }
        //public ObservableCollection<DocData> docList
        //{
        //    get
        //    {

        //        //var doc = db.DocDatas.Where(x => x.DocSubject.Contains(cmbSubject.Text) && x.DocControlNumber.Contains(cmbControlNumber.Text) && x.DoctTypes.Contains(cmbDocType.Text) && x.DateAdd.Equals(dtpDate.SelectedDate)).ToList();
        //        var doc = db.DocDatas.ToList();
        //        var docList = new ObservableCollection<DocData>();
        //        {
        //            foreach (var item in doc)
        //            {
        //                docList.Add(new DocData()
        //                {
        //                    Id = item.Id,
        //                    DocSubject = item.DocSubject,
        //                    DocControlNumber = item.DocControlNumber,
        //                    DoctTypes = item.DoctTypes,
        //                    DateAdd = item.DateAdd
        //                });
        //            }
        //            return docList;
        //        }
        //    }
        //}
        
        private void buttonShowDialog(object sender, RoutedEventArgs e) //event for clicking add button
        {
            var item = sender as Button;

            this.Opacity = .6;
            this.Background = Brushes.Black;
            var dialog = new dDocInfo(int.Parse(item.Tag.ToString()));
            dialog.ShowDialog();
            this.Opacity = 1;
            this.Background = Brushes.Transparent;
        }
        public void loadDocList()
        {

            sampleDocs.Clear();
            var doc = db.DocDatas.ToList();         

            if (!string.IsNullOrWhiteSpace(cmbControlNumber.Text))
                doc = doc.Where(x => x.DocControlNumber.Contains(cmbControlNumber.Text)).ToList();

            if (!string.IsNullOrWhiteSpace(cmbSubject.Text))
                doc = doc.Where(x => x.DocSubject.Contains(cmbSubject.Text)).ToList();

            if (!string.IsNullOrWhiteSpace(cmbDocType.Text))
                doc = doc.Where(x => x.DoctTypes.Contains(cmbDocType.Text)).ToList();

            if (!string.IsNullOrWhiteSpace(dtpDate.Text))
                doc = doc.Where(x => x.DateAdd.Contains(DateTime.Parse(dtpDate.Text).ToString("yyyy-MM-dd"))).ToList();
            var docList = new ObservableCollection<DocData>();
            {
                foreach (var item in doc)
                {
                    docList.Add(new DocData()
                    {
                        Id = item.Id,
                        DocSubject = item.DocSubject,
                        DocControlNumber = item.DocControlNumber,
                        DoctTypes = item.DoctTypes,
                        DateAdd = item.DateAdd
                    });
                    sampleDocs.Add(item);
                }
            }
        }
        public void btnSearch_Click(object sender, RoutedEventArgs eventArgs)
        {
            loadDocList();
            
        }
    }
}
