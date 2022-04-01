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
    /// Interaction logic for dDocInfo.xaml
    /// </summary>
    public partial class dDocInfo : Window
    {
        public dDocInfo(int id)
        {
            InitializeComponent();
            docDataId = id;

            lblDocTitle.Content = db.DocDatas.Where(x => x.Id.Equals(id)).Select(x => x.Tag).FirstOrDefault().Equals("I") ? "INCOMING" : "OUTGOING";
            changeLabel();
            this.DataContext = this;
        }
        dbDocs db = new dbDocs();
        int docDataId;
        List<string> focalName = new List<string>();
        List<string> focalOffice = new List<string>();
        private void changeLabel()
        {
            if (lblDocTitle.Content.ToString().Contains("OUTGOING"))
            {
                lblLabelAddresseeName.Content = "ADDRESSEE/FULL NAME";
                lblLabelAddresseeOffice.Content = "ADDRESSEE/OFFICE";
                lblLabelDateAdded.Content = "FOR SIGNATURE";
                lblLabelForSign.Content = "DATE ADDED";
                lblLabelSigned.Content = "SIGNED";
                lblLabelReleased.Content = "RELEASED";
                lblLabelOriginFocal.Content = "ORIGIN/FOCAL";
                lblLabelOriginOffice.Content = "ORIGIN/OFFICE";
                lblLabelControlNumber.Content = "CONTROL NUMBER";
            }
            else
            {
                lblLabelAddresseeName.Content = "ORIGIN/FULL NAME";
                lblLabelAddresseeOffice.Content = "ORIGIN/OFFICE";
                lblLabelDateAdded.Content = "RECEIVED BY ROD";
                lblLabelForSign.Content = "DATE OF DOCUMENT";
                lblLabelSigned.Content = "RECEIVED BY FOCAL";
                lblLabelReleased.Content = "DATE OF ACTION";
                lblLabelOriginFocal.Content = "ASSIGNED/FOCAL(S)";
                lblLabelOriginOffice.Content = "ASSIGNED/OFFICE";
                lblLabelControlNumber.Content = "ORD | ROD | DOCUMENT NUMBER";
            }
        }
        public void focalData()
        {
            var focal = db.DocDatas.Where(x => x.Id.Equals(docDataId)).Select(x => x.FocalID).FirstOrDefault().Split(',').ToList();
            foreach (var item in focal)
            {
                var id = int.Parse(item);
                var fName = "";
                if (lblDocTitle.Content.ToString().Contains("INCOMING"))
                    fName = db.Focals.Where(x => x.Id.Equals(id)).Select(x => x.NickName).FirstOrDefault();
                else
                    fName = db.Focals.Where(x => x.Id.Equals(id)).Select(x => x.FullName).FirstOrDefault();
                focalName.Add(fName);
                
                focalOffice.Add(db.Focals.Where(x => x.Id.Equals(id)).FirstOrDefault().Office.OperatingUnit);
            }
            
        }
        public ObservableCollection<IncomingClass> doctDatas
        {
            get
            {
                var doc = new DocData();
                doc.Id = docDataId;
                focalData();
                var doctList = new ObservableCollection<IncomingClass>();
                foreach (var item in doc.GetDocDatas())
                {
                    DateTime newForSign = item.ForSigned ?? DateTime.Now;
                    doctList.Add(new IncomingClass()
                    {
                        DocDataID = item.Id,
                        DocSubject = item.DocSubject,
                        Status = item.CurrentStatus,
                        DocumentType = item.DoctTypes,
                        ORDNumber = item.DocControlNumber,
                        Tag = item.Tag,
                        DateAdded = item.DateAdd,
                        DateOfDocument = lblDocTitle.Content.ToString().Contains("OUTGOING") ? item.DateAdd : newForSign,
                        DateReceivedByFocal = item.Signed,
                        ActionDate = item.ForRelease,
                        Remarks1 = lblDocTitle.Content.ToString().Contains("OUTGOING") ? item.Remarks : item.Remarks.Split('|')[0],
                        Remarks2 = lblDocTitle.Content.ToString().Contains("OUTGOING") ? item.Remarks : item.Remarks.Split('|')[1],
                        FilePath = item.DocPaths.Select(x => x.Path).FirstOrDefault(),
                        OriginID = item.Addressee.Id,
                        OriginOffice = item.Addressee.Office,
                        OriginSignatory = item.Addressee.FullName,
                        FocalID = string.Join(", ", focalName.Distinct()),
                        FocalOffice = string.Join(", ", focalOffice.Distinct())
                    });


                    if (lblDocTitle.Content.ToString().Contains("OUTGOING"))
                    {
                        if (!string.IsNullOrWhiteSpace(item.Signed.ToString()))
                        {
                            btnSigned.Visibility = Visibility.Visible;
                            btnSigned.Tag = item.DocPaths.Where(x => x.DocStatusTag.Equals("S")).Select(x => x.Path).First();
                        }

                        if (!string.IsNullOrWhiteSpace(item.ForRelease.ToString()))
                        {
                            btnReceived.Visibility = Visibility.Visible;
                            btnReceived.Tag = item.DocPaths.Where(x => x.DocStatusTag.Equals("R")).Select(x => x.Path).First();
                        }
                    }
                    else
                    {
                        if (item.DocPaths.Count() > 0)
                        {
                            btnForSigned.Visibility = Visibility.Visible;
                            btnForSigned.Tag = item.DocPaths.Where(x => x.DocStatusTag.Equals("R")).Select(x => x.Path).First();
                        }
                    }
                }            

               
                return doctList;
            }
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

        private void buttonShowPDF(object sender, RoutedEventArgs e)
        {
                var btn = sender as Button;
                var pdfView = new dPDFView(btn.Tag.ToString());
                Console.WriteLine(btn.Tag);
                pdfView.Show();
        }
    }
}
