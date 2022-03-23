using DocsControl.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using static DocsControl.Model.Modules;

namespace DocsControl.Dialogs
{
    /// <summary>
    /// Interaction logic for dAddEditDocs.xaml
    /// </summary>
    public partial class dAddEditDocs : Window
    {

        public class path
        {
            public string signed { get; set; }
            public string received { get; set; }
        }
        public dAddEditDocs(string dialogName, int id)
        {
            InitializeComponent();
            lblTitle.Content = dialogName;
            this.docDataID = id;
            //docData.Id = id;

            var focals = db.Focals.Select(x => x.FullName).ToList(); //populate combo box with focal names
            ComboBox(cmbFocals, focals); //method for populating //Models>Modules static

            //load info if the form is in editing view
            //if (dialogName.Contains("EDIT"))
            //{
            //   //oadInfo();
            //    addresseeID = docData.GetDocDatas().FirstOrDefault().AddresseeID;
            //}

            DataContext = new
            {
                docInfo = doctDatas,
                addresseeInfo = addressees
            };
        }

        dbDocs db = new dbDocs(); //instantiate Model Docs
        Addressee addressee = new Addressee();
        DocData docData = new DocData();
        DocPath docPath = new DocPath();
        private int docDataID;
        private string currentFileName;
        private string destinationPath;
        private DateTime? signedDate;
        private DateTime? receivedDate;
        private string signedCopyFile;
        private string receivedCopyFile;
  
        public ObservableCollection<DocData> doctDatas 
        {
            get
            {
                var doc = new DocData();
                doc.Id = docDataID;
   
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
                docPath.DocDataID = docDataID;

                if (docPath.GetDocPaths("S").Count() > 0) //get file name and trim (if there's any)
                    signedCopyFile = new FileInfo(docPath.GetDocPaths("S").FirstOrDefault().Path).Name;
                else
                    signedCopyFile = "...";

                if (docPath.GetDocPaths("R").Count() > 0) //get file name and trim (if there's any)
                    receivedCopyFile = new FileInfo(docPath.GetDocPaths("R").FirstOrDefault().Path).Name;
                else
                    receivedCopyFile = "...";

                //bind file name (if there's any)
                lblSigned.Content = lblSigned.Content.ToString().Contains("ADD") ? "..." : signedCopyFile;
                lblReceived.Content = lblReceived.Content.ToString().Contains("ADD") ? "..." : receivedCopyFile;


                //change of content and color in buttons
                if (!lblSigned.Content.Equals("..."))
                {
                    btnSigned.Content = "REMOVE";
                    btnSigned.Background = Brushes.OrangeRed;
                }
                if (!lblReceived.Content.Equals("..."))
                {
                    btnReceived.Content = "REMOVE";
                    btnReceived.Background = Brushes.OrangeRed;
                }

                //return docdata
                return doctList;
            }
        }

        public ObservableCollection<Addressee> addressees //collection of addressees
        {
            get
            {
                var doc = new DocData();
                doc.Id = docDataID;

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
        bool isValid() //validation of inputs before saving. skip if no errors
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text) ||
                string.IsNullOrWhiteSpace(cmbStatus.Text) ||    
                string.IsNullOrWhiteSpace(cmbFocals.Text) ||
                string.IsNullOrWhiteSpace(txtDocType.Text) ||
                string.IsNullOrWhiteSpace(txtOffice.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                showError("Please fill out all necessary fields (Subject, Status, Focal, Type, Office, Full Name, Email)");
                return false;
            }

            //saving validation of signed copy
            if(cmbStatus.Text.Contains("SIGNED") && (btnSigned.Content.ToString().Contains("ADD") || string.IsNullOrWhiteSpace(dpSigned.Text) || string.IsNullOrWhiteSpace(tpSigned.Text)))
            {
                showError("Save Failed: Please input date and time of Signed Copy");
                return false;
            }

            //saving validation of received copy
            if (cmbStatus.Text.Contains("RECEIVED") && (btnReceived.Content.ToString().Contains("ADD") || string.IsNullOrWhiteSpace(dpReceived.Text) || string.IsNullOrWhiteSpace(tpReceived.Text)))
            {
                showError("Save Failed: Please input date and time of Received Copy");
                return false;
            }

            return true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }               
        private bool browseFile()
        {
            //open dialog tool to open a file then store the filename into label
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Desktop";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                currentFileName = openFileDialog.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }
        private void uploadFile()  
        {
            //try
            //{
                //getting the current path
                string signedCopy = lblSigned.Content.ToString();
                string receivedCopy = lblReceived.Content.ToString();
                //setting the destination path
                string destinationDirectory = @"\\R4A_FileServer\Control of Documents\";

                //combine current and destination path then copy            
                if (!btnReceived.Content.ToString().Contains("ADD") && lblReceived.Content.ToString().Contains(@"\"))
                {
                    var f = new FileInfo(receivedCopy);
                    File.Copy(receivedCopy, destinationDirectory + string.Format("{0}-{1}", "RECEIVED" + DateTime.Now.ToString("yyyyMMddHHmm"), f.Name), true);//copy and renaming of file
                    destinationPath = destinationDirectory + string.Format("{0}-{1}", "RECEIVED" + DateTime.Now.ToString("yyyyMMddHHmm"), f.Name);//storing the path to database
                    docPath.pathList.Add(string.Format("{0}|{1}", destinationPath, "R"));//add to list 
                }
                if (!btnSigned.Content.ToString().Contains("ADD") && lblSigned.Content.ToString().Contains(@"\"))
            {
                    var f = new FileInfo(signedCopy);
                    File.Copy(signedCopy, destinationDirectory + string.Format("{0}-{1}", "SIGNED" + DateTime.Now.ToString("yyyyMMddHHmm"), f.Name), true);
                    destinationPath = destinationDirectory + string.Format("{0}-{1}", "SIGNED" + DateTime.Now.ToString("yyyyMMddHHmm"), f.Name);
                    docPath.pathList.Add(string.Format("{0}|{1}", destinationPath, "S"));
                }                                 
        }
        private void btnSigned_Click(object sender, RoutedEventArgs e)
        {
            changeButtonContent(btnSigned, "ADD SIGNED COPY", "REMOVE", dpSigned, tpSigned, lblSigned);                       
        }
        private void btnReceived_Click(object sender, RoutedEventArgs e)
        {
            changeButtonContent(btnReceived, "ADD RECEIVED COPY", "REMOVE", dpReceived, tpReceived, lblReceived);
        }
        private void changeButtonContent(Button btn, string currentContent, string newcontent, DatePicker dp, TimePicker tp, Label lbl)
        {            
            if (btn.Content.ToString().Contains("ADD")) //
            {
                if (!browseFile())
                    return;
                btn.Content = newcontent;
                dp.SelectedDate = DateTime.Now;
                tp.SelectedTime = DateTime.Now;
                lbl.Content = currentFileName;
                btn.Background = Brushes.OrangeRed;
            }
            else
            {
                if (lblTitle.Content.ToString().Contains("EDIT"))
                {
                    if (btn.Content.ToString().Contains("REMOVE") && !lbl.Content.ToString().Contains(@"\"))
                    {
                        if (showWarning("DO YOU WANT TO REMOVE THIS FILE?").Equals(true))
                        {                            
                            //store the path in the list to be deleted/removed
                            var itemPath = docPath.GetDocPaths(lbl.Content.ToString()[0].ToString()).FirstOrDefault().Path;                            
                            docPath.pathItem.Add(itemPath);                                                                              
                        }
                        else                        
                            return;                                                
                    }
                }
                btn.Content = currentContent;
                dp.SelectedDate = null;
                tp.SelectedTime = null;
                lbl.Content = "...";
                btn.Background = Brushes.Teal;
            }
        }
        private void bindAddressee()
        {
            addressee = new Addressee()
            {
                ContactNo = txtContact.Text,
                Email = txtEmail.Text,
                FullName = txtFullName.Text,
                Office = txtOffice.Text,
                Id = lblTitle.Content.ToString().Contains("ADD") ? 0 : int.Parse(lblAddressee.Tag.ToString())
            };
        }
        private void bindDocData()
        {
            docData = new DocData()
            {
                Id = docDataID,
                DocSubject = txtSubject.Text,
                CurrentStatus = cmbStatus.Text,
                ForSigned = DateTime.Now,
                Signed = signedDate,
                ForRelease = receivedDate,
                FocalID = db.Focals.Where(x => x.FullName.Equals(cmbFocals.Text)).FirstOrDefault().Id,
                DateAdd = DateTime.Now.ToString("yyyy-MM-dd HH:mmtt"),
                DoctTypes = txtDocType.Text,
                AddresseeID = lblTitle.Content.ToString().Contains("ADD") ? db.Addressees.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() : int.Parse(lblAddressee.Tag.ToString()),
                Remarks = txtRemarks.Text,
                Tag = "O",
                DocControlNumber = txtControlNumber.Text
            };
        }
        private void updateDatabase()
        {
            //this method is to store data to corresponding models

            bindAddressee();
            if (lblTitle.Content.ToString().Contains("ADD"))
                addressee.addAddressee();
            else
                addressee.editAddressee();

            if (btnSigned.Content.ToString().Contains("ADD"))
                signedDate = null;
            else
                signedDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(dpSigned.Text).ToString("yyyy-MM-dd"), DateTime.Parse(tpSigned.Text).ToString("HH:mm:ss")));         

            if (btnReceived.Content.ToString().Contains("ADD"))
                receivedDate = null;
            else
                receivedDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(dpReceived.Text).ToString("yyyy-MM-dd"), DateTime.Parse(tpReceived.Text).ToString("HH:mm:ss")));

          
            bindDocData();

            if (lblTitle.Content.ToString().Contains("ADD"))
                docData.addDocData();
            else
                docData.editDocData();

            //upload file method
            uploadFile();

            //insert new data in DocPath

            docPath.DocDataID = docDataID == 0 ? db.DocDatas.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() : docDataID;
            docPath.addPath();

            if (docPath.pathItem.Count > 0)
            {
                docPath.deletePath();                
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!isValid())
                return;
            else
            {
                try
                {
                    updateDatabase();
                    showInfo("Successfully Saved!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    showError("An error has occured" + ex.Message);
                    return;
                }
            }
        }

        



        //the process should be explained here...
    }
}
