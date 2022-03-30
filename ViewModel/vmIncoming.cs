using DocsControl.Dialogs;
using DocsControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var item = sender as Button;
            string title = "";
            int DocID = 0;
            if (item.Content.Equals("VIEW"))
            {
                title = "EDIT INCOMING DOCUMENT";
                DocID = int.Parse(item.Tag.ToString());
            }
            else
                title = "ADD INCOMING DOCUMENT";

            this.Opacity = .5;
            this.Background = Brushes.Black;
            var dialog = new dAddEditDocs2(title, DocID);
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
            string focalID = "";
            var doc = db.DocDatas.Where(x => x.Tag.Equals("I")).ToList();

            if (!string.IsNullOrWhiteSpace(cmbORDRODNumber.Text))
                doc = doc.Where(x => x.DocControlNumber.Contains(cmbORDRODNumber.Text)).ToList();

            if (!string.IsNullOrWhiteSpace(cmbSubject.Text))
                doc = doc.Where(x => x.DocSubject.Contains(cmbSubject.Text)).ToList();

            if (!string.IsNullOrWhiteSpace(cmbPersonnel.Text))
            {
                //if (cmbPersonnel.Text.Contains(","))
                //{
                //    var cmbfocals = cmbPersonnel.Text.Split(new char[] { ',', ' ' }).ToList();
                //    var newFocals = new List<int>(); //list of focals in combo box
                //    foreach (var item in cmbfocals)
                //    {
                //        if (!item.Equals(""))
                //        {
                //            var fID = db.Focals.Where(x => x.NickName.Contains(item)).FirstOrDefault().Id;
                //            newFocals.Add(fID);
                //        }
                //    }

                //    //sort the list of id in combo box
                //    //concat them in a string
                //    //fill out all missing number
                //    //ex, the lowest is 10 then the highest if 13. numbers 11 and 12 should be inserted
                //    //thats the result 

                //    foreach (var item in newFocals.Distinct().OrderBy(x =>x))
                //    {
                //        Console.WriteLine(item);
                //    }
                //    var firstIndex = newFocals.Distinct().OrderBy(x => x).FirstOrDefault();
                //    var lastIndex = newFocals.Distinct().OrderByDescending(x => x).FirstOrDefault();

                //    var hatdog = new List<string>();
                //    for (int i = firstIndex; i <= lastIndex; i++)
                //    {
                //        hatdog.Add(i.ToString());
                //    }

                //    var focalids = doc.Select(x => x.FocalID).ToList();
                                        
                   
                //} 
                doc = doc.Where(x => x.FocalID.Contains(cmbPersonnel.Text)).ToList();
            }
                     
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
            //if (!string.IsNullOrWhiteSpace(cmbPersonnel.Text))
            //{
            //    if (cmbPersonnel.Text.Contains(","))
            //    {
            //        var cmbfocals = cmbPersonnel.Text.Split(new char[] { ',', ' ' }).ToList();
            //        var newFocals = new List<int>(); //list of focals in combo box
            //        foreach (var item in cmbfocals)
            //        {
            //            if (!item.Equals(""))
            //            {
            //                var fID = db.Focals.Where(x => x.NickName.Contains(item));
            //                if (fID.Count() > 0)
            //                {
            //                    var f = fID.FirstOrDefault().Id;
            //                    newFocals.Add(f);
            //                }
                            
            //            }
            //        }

            //        //sort the list of id in combo box
            //        //concat them in a string
            //        //fill out all missing number
            //        //ex, the lowest is 10 then the highest if 13. numbers 11 and 12 should be inserted
            //        //thats the result 
            //        var firstIndex = newFocals.Distinct().OrderBy(x => x).FirstOrDefault();
            //        var lastIndex = newFocals.Distinct().OrderByDescending(x => x).FirstOrDefault();

            //        var hatdog = new List<string>();
            //        for (int i = firstIndex; i <= lastIndex; i++)
            //        {
            //            hatdog.Add(i.ToString());
            //        }
            //        Console.WriteLine(string.Join(",",hatdog));


            //        //just implement that damn frenzy search



                    
            //        //focalids.ForEach(x => x.FocalID = string.Join(",", zz));
            //        //Console.WriteLine(newFocals.Distinct().OrderBy(x => x).FirstOrDefault() + " " + newFocals.Distinct().OrderByDescending(x => x).FirstOrDefault());
            //        //doc = doc.Where(x => x.FocalID.Contains(focalID)).ToList();
            //    }
            //}
        }


        //this method is used if u want to get rid of all the fucking spaces in a string.
        //var focal = string.ToCharArray().Where(x => !Char.IsWhiteSpace(x)).Select(x => x.ToString()).Aggregate((x, z) => x + z);
    }
}
