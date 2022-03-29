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

            DataContext = new
            {
                docInfo = doctDatas,
                routeInfo = routes
            };
        }
        int docDataId;
        int focalId;
        public ObservableCollection<DocData> doctDatas
        {
            get
            {
                var doc = new DocData();
                doc.Id = docDataId;

                var doctList = new ObservableCollection<DocData>();
                foreach (var item in doc.GetDocDatas())
                {
                    switch (item.CurrentStatus) //selected index for current status
                    {
                        case "FOR SIGNATURE":
                            item.CurrentStatus = "0";
                            break;
                        case "SIGNED":
                            item.CurrentStatus = "1";
                            break;
                        case "RECEIVED":
                            item.CurrentStatus = "2";
                            break;
                    }
                    focalId = int.Parse(item.FocalID);
                    doctList.Add(new DocData()
                    {
                        Id = item.Id,
                        DocSubject = item.DocSubject,
                        CurrentStatus = item.CurrentStatus,
                        DoctTypes = item.DoctTypes,
                        DocControlNumber = item.DocControlNumber,
                        Remarks = item.Remarks,
                        DateAdd = item.DateAdd,
                        Tag = item.Tag,
                        ForSigned = item.ForSigned,
                        Signed = item.Signed,
                        ForRelease = item.ForRelease,
                        FocalID = (int.Parse(item.FocalID) - 1).ToString(),
                    });

                    if (!string.IsNullOrWhiteSpace(item.Signed.ToString()))
                    {
                        btnSigned.Visibility = Visibility.Visible;
                        btnSigned.Tag = item.DocPaths.Where(x =>x.DocStatusTag.Equals("S")).Select(x => x.Path).First();
                    }                        

                    if (!string.IsNullOrWhiteSpace(item.ForRelease.ToString()))
                    {
                        btnReceived.Visibility = Visibility.Visible;
                        btnReceived.Tag = item.DocPaths.Where(x => x.DocStatusTag.Equals("R")).Select(x => x.Path).First();
                    }
                }

                //this code will load the file path
                //docPath.DocDataID = docDataID;

                //if (docPath.GetDocPaths("S").Count() > 0) //get file name and trim (if there's any)
                //    signedCopyFile = new FileInfo(docPath.GetDocPaths("S").FirstOrDefault().Path).Name;
                //else
                //    signedCopyFile = "...";

                //if (docPath.GetDocPaths("R").Count() > 0) //get file name and trim (if there's any)
                //    receivedCopyFile = new FileInfo(docPath.GetDocPaths("R").FirstOrDefault().Path).Name;
                //else
                //    receivedCopyFile = "...";

                ////bind file name (if there's any)
                //lblSigned.Content = lblSigned.Content.ToString().Contains("ADD") ? "..." : signedCopyFile;
                //lblReceived.Content = lblReceived.Content.ToString().Contains("ADD") ? "..." : receivedCopyFile;


            

                //return docdata
                return doctList;
            }
        }        

        public ObservableCollection<Routes> routes //collection of addressees
        {
            get
            {
                var db = new dbDocs();
                var focal = db.DocDatas.Where(x => x.Id.Equals(docDataId)).ToList();
                var addresseeList = new ObservableCollection<Routes>();
                foreach (var item in focal)
                {
                    addresseeList.Add(new Routes()
                    {
                       // FocalName = item.Focal.FullName,
                       // OriginOffice = item.Focal.Office.OperatingUnit,
                        AddresseeName = item.Addressee.FullName,
                        AddresseeOffice = item.Addressee.Office
                    });
                }
                return addresseeList;
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
