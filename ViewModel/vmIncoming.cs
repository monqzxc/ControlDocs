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
using System.Windows.Media;
using static DocsControl.Model.Modules;

namespace DocsControl.ViewModel
{
    public partial class vmIncoming
    {

        public vmIncoming()
        {
            InitializeComponent();
            loadComboBox();
            loadDocList();
            this.DataContext = this;
        }
        dbDocs db = new dbDocs();

        private void loadComboBox()
        {
            var subjects = db.DocDatas.Where(x => x.Tag.Equals("I")).Select(x => x.DocSubject).ToList();
            var focals = db.Focals.Select(x => x.NickName).ToList();
            var controlNumber = db.DocDatas.Where(x => x.Tag.Equals("I")).Select(x => x.DocControlNumber).ToList();
            var controlNumberList = new List<string>();
            foreach (var item in controlNumber)
            {
                var cn = item.Split('|');
                controlNumberList.Add(string.Format("{0} | {1}", cn[0], cn[1]));
            }

            ComboBox(cmbSubject, subjects);
            ComboBox(cmbPersonnel, focals);
            ComboBox(cmbORDRODNumber, controlNumberList);

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
        private ObservableCollection<DocData> sampleDocs = new ObservableCollection<DocData>();
        public ObservableCollection<DocData> DocDatas
        {
            get { return sampleDocs; }
            set { sampleDocs = value; }
        }
        public void loadDocList()
        {
            
            sampleDocs.Clear();
            var dd = new DocData();
            var doc = db.DocDatas.Where(x => x.Tag.Equals("I")).ToList();

            if (!string.IsNullOrWhiteSpace(cmbORDRODNumber.Text))
                doc = doc.Where(x => x.DocControlNumber.Contains(cmbORDRODNumber.Text)).ToList();

            if (!string.IsNullOrWhiteSpace(cmbSubject.Text))
                doc = doc.Where(x => x.DocSubject.Contains(cmbSubject.Text)).ToList();

            //if (!string.IsNullOrWhiteSpace(cmbPersonnel.Text))
            //    doc = doc.Where(x => x.DoctTypes.Contains(cmbDocType.Text)).ToList();

            if (!string.IsNullOrWhiteSpace(dtpDate.Text))
            {
                DateTime fromDate = new DateTime(dtpDate.SelectedDate.Value.Year, dtpDate.SelectedDate.Value.Month, dtpDate.SelectedDate.Value.Day, 00, 00, 00);
                DateTime toDate = new DateTime(dtpDate.SelectedDate.Value.Year, dtpDate.SelectedDate.Value.Month, dtpDate.SelectedDate.Value.Day, 23, 59, 59);
                doc = doc.Where(x => x.DateAdd >= fromDate && x.DateAdd <= toDate).ToList();
            }

            foreach (var item in doc)
            {
                              
                dd.Id = item.Id;

                var cn = item.DocControlNumber.Split('|');
                sampleDocs.Add(new DocData()
                {
                    Id = item.Id,
                    DocSubject = item.DocSubject,
                    DocControlNumber = string.Format("{0} | {1}", cn[0], cn[1]),
                    FocalID = string.Join(", ", dd.GetFocals()),
                    DateAdd = item.DateAdd
                });
            }

            lblTotalDocs.Content = string.Format("TOTAL DOCUMENT(S): {0}", sampleDocs.Count());
            //totalDocs = docList.Count();
        }
        public void btnSearch_Click(object sender, RoutedEventArgs eventArgs)
        {
            loadDocList();
        }
    }
}
