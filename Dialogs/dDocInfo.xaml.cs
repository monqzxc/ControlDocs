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
                addresseeInfo = addressees
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
                    focalId = item.FocalID;
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
                        FocalID = item.FocalID - 1,
                    });
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
        public ObservableCollection<Addressee> addressees //collection of addressees
        {
            get
            {
                var doc = new DocData();
                doc.Id = docDataId;

                var addresseeList = new ObservableCollection<Addressee>();
                foreach (var item in doc.GetDocDatas())
                {
                    addresseeList.Add(new Addressee()
                    {
                        Id = item.Addressee.Id,
                        Office = item.Addressee.Office,
                        FullName = item.Addressee.FullName,
                        Email = item.Addressee.Email,
                        ContactNo = item.Addressee.ContactNo
                    });
                }
                return addresseeList;
            }
        }

        public ObservableCollection<Focal> focals //collection of addressees
        {
            get
            {
                var focal = new Focal();
                focal.Id = focalId;

                var addresseeList = new ObservableCollection<Focal>();
                foreach (var item in focal.GetFocals())
                {
                    addresseeList.Add(new Focal()
                    {
                        Id = item.Id,
                        Office = item.Office,
                        FullName = item.FullName,
                        Email = item.Email,
                        ContactNumber = item.ContactNumber,
                        PlantillaID = item.PlantillaID,
                        FocalshipID = item.FocalshipID,
                        OfficeID = item.OfficeID
                    });
                }
                return addresseeList;
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
