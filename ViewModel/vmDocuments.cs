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
            {
                DateTime fromDate = new DateTime(dtpDate.SelectedDate.Value.Year, dtpDate.SelectedDate.Value.Month, dtpDate.SelectedDate.Value.Day, 00, 00, 00);
                DateTime toDate = new DateTime(dtpDate.SelectedDate.Value.Year, dtpDate.SelectedDate.Value.Month, dtpDate.SelectedDate.Value.Day, 23, 59, 59);
                doc = doc.Where(x => x.DateAdd >= fromDate && x.DateAdd <= toDate).ToList();
            }

            foreach (var item in doc.OrderByDescending(x => x.Id))
            {
                //control number will depend on tag of document. split and concat with Incoming then stay as is when Outgoing
                var cn = item.Tag.Equals("I") ? string.Format("{0} | {1}", item.DocControlNumber.Split('|')[0], item.DocControlNumber.Split('|')[1])  :  item.DocControlNumber;
                sampleDocs.Add(new DocData()
                {
                    Id = item.Id,
                    DocSubject = item.DocSubject,
                    DocControlNumber = cn,
                    DoctTypes = item.DoctTypes,
                    DateAdd = item.DateAdd
                });
            }

            lblTotalDocs.Content = string.Format("TOTAL DOCUMENT(S): {0}", sampleDocs.Count());

        }
        public void btnSearch_Click(object sender, RoutedEventArgs eventArgs)
        {
            loadDocList();
        }
        int totalDocs;
        public void pagination()
        {
            var pages = totalDocs >= 12 ? totalDocs / 12 : 1; //if the total items is more than 12, generate new page. else 1 page
            for (int i = 0; i < totalDocs; i++)
            {

            }
        }
    }
}
