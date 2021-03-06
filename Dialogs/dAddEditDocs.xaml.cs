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
  
        public dAddEditDocs(string dialogName, int id, string user)
        {
            InitializeComponent();
            lblTitle.Content = dialogName;
            this.docDataID = id;
            this.user = user;
            this.nickName = user.Split('|')[1];
            this.role = int.Parse(user.Split('|')[0]);
            //docData.Id = id;
            this.user = user;
            var focals = db.Focals.OrderBy(x => x.PlantillaID).Select(x => x.FullName).ToList(); //populate combo box with focal names
            ComboBox(cmbFocals, focals); //method for populating //Models>Modules static           

            if (role > 2)
            {
                grid1.IsEnabled = false;
                grid2.IsEnabled = false;
                grid3.IsEnabled = false;
            }
                DataContext = new
            {
                docInfo = doctDatas,
                addresseeInfo = addressees
            };
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton.Equals(MouseButton.Left))
                DragMove();
        }

        dbDocs db = new dbDocs(); //instantiate Model Docs
        Addressee addressee = new Addressee();
        DocData docData = new DocData();
        DocPath docPath = new DocPath();
        Activities activities = new Activities();
        private int docDataID;
        private string currentFileName;
        private DateTime? signedDate;
        private DateTime? receivedDate;
        private string signedCopyFile;
        private string receivedCopyFile;
        private string user;
        private string nickName;
        private int role;

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
                        FocalID = (int.Parse(item.FocalID) - 1).ToString(),                                               
                    });
                }

                //this code will load the file path
                docPath.DocDataID = docDataID;

                if (docPath.GetDocPaths("S").Count() > 0) //get file name and trim (if there's any){
                { 
                    signedCopyFile = new FileInfo(docPath.GetDocPaths("S").FirstOrDefault().Path).Name;
                    lblSigned.Cursor = Cursors.Hand;
                    lblSigned.Tag = docPath.GetDocPaths("S").FirstOrDefault().Path;
                }
                else
                {
                    signedCopyFile = "...";
                    lblSigned.Cursor = Cursors.Arrow;
                    lblSigned.Tag = "...";
                }


                if (docPath.GetDocPaths("R").Count() > 0) //get file name and trim (if there's any)
                {
                    receivedCopyFile = new FileInfo(docPath.GetDocPaths("R").FirstOrDefault().Path).Name;
                    lblReceived.Cursor = Cursors.Hand;
                    lblReceived.Tag = docPath.GetDocPaths("R").FirstOrDefault().Path;
                }
                else
                {
                    receivedCopyFile = "...";
                    lblReceived.Cursor = Cursors.Arrow;
                    lblReceived.Tag = "...";
                }
                    

                //bind file name (if there's any)
                lblSigned.Text = lblSigned.Text.Contains("ADD") ? "..." : signedCopyFile;
                lblReceived.Text = lblReceived.Text.Contains("ADD") ? "..." : receivedCopyFile;
                            
                
                //change of content and color in buttons
                if (!lblSigned.Text.Equals("..."))
                {
                    btnSigned.Content = "REMOVE";
                    btnSigned.Background = Brushes.OrangeRed;
                }
                if (!lblReceived.Text.Equals("..."))
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
            if (showWarning("DO YOU WANT TO CANCEL?").Equals(true))
            {
                this.Close();
            }
            else
                return;        
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
        
        private void btnSigned_Click(object sender, RoutedEventArgs e)
        {
            changeButtonContent(btnSigned, "ADD SIGNED COPY", "REMOVE", dpSigned, tpSigned, lblSigned);                       
        }
        private void btnReceived_Click(object sender, RoutedEventArgs e)
        {
            changeButtonContent(btnReceived, "ADD RECEIVED COPY", "REMOVE", dpReceived, tpReceived, lblReceived);
        }
        private void changeButtonContent(Button btn, string currentContent, string newcontent, DatePicker dp, TimePicker tp, AccessText lbl)
        {            
            if (btn.Content.ToString().Contains("ADD")) //
            {
                if (!browseFile())
                    return;
                btn.Content = newcontent;
                dp.SelectedDate = DateTime.Now;
                tp.SelectedTime = DateTime.Now;
                lbl.Text = currentFileName;
                lbl.Cursor = Cursors.Hand;
                btn.Background = Brushes.OrangeRed;
            }
            else
            {
                if (lblTitle.Content.ToString().Contains("EDIT"))
                {
                    if (btn.Content.ToString().Contains("REMOVE") && !lbl.Text.Contains(@"\"))
                    {
                        if (showWarning("DO YOU WANT TO REMOVE THIS FILE?").Equals(true))
                        {
                            //store the path in the list to be deleted/removed
                            lbl.Cursor = Cursors.Arrow;
                            var itemPath = docPath.GetDocPaths(lbl.Text[0].ToString()).FirstOrDefault().Path; //getting the first index of string and make it the TAG.
                            docPath.pathItem.Add(itemPath);                                                                              
                        }
                        else                        
                            return;                                                
                    }
                }
                btn.Content = currentContent;
                dp.SelectedDate = null;
                tp.SelectedTime = null;
                lbl.Text = "...";
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
                ForSigned = lblTitle.Content.ToString().Contains("EDIT") ? DateTime.Parse(txtSubject.Tag.ToString()) : DateTime.Now,
                Signed = signedDate,
                ForRelease = receivedDate,
                FocalID = db.Focals.Where(x => x.FullName.Equals(cmbFocals.Text)).FirstOrDefault().Id.ToString(),
                DateAdd = lblTitle.Content.ToString().Contains("EDIT") ? DateTime.Parse(txtSubject.Tag.ToString()) : DateTime.Now,
                DoctTypes = txtDocType.Text,
                AddresseeID = lblTitle.Content.ToString().Contains("ADD") ? db.Addressees.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() : int.Parse(lblAddressee.Tag.ToString()),
                Remarks = txtRemarks.Text,
                Tag = "O",
                DocControlNumber = txtControlNumber.Text
            };
        }

        private void uploadFile()
        {
            //getting the current path
            string signedCopy = lblSigned.Text;
            string receivedCopy = lblReceived.Text;
            //setting the destination path
            string destinationDirectory = @"\\R4A_FileServer\Control of Documents\";

            //combine current and destination path then copy            
            if (!btnReceived.Content.ToString().Contains("ADD") && lblReceived.Text.Contains(@"\"))
            {
                var f = new FileInfo(receivedCopy);
                var fileName = destinationDirectory + string.Format("{0}-{1}", "RECEIVED" + DateTime.Now.ToString("yyyyMMddHHmm"), f.Name);
                File.Copy(receivedCopy, fileName, true);//copy and renaming of file               

                docPath.pathList.Add(string.Format("{0}|{1}", fileName, "R"));//add to list 
            }

            if (!btnSigned.Content.ToString().Contains("ADD") && lblSigned.Text.Contains(@"\"))
            {
                var f = new FileInfo(signedCopy);
                var fileName = destinationDirectory + string.Format("{0}-{1}", "SIGNED" + DateTime.Now.ToString("yyyyMMddHHmm"), f.Name);
                File.Copy(signedCopy, fileName, true);
                docPath.pathList.Add(string.Format("{0}|{1}", fileName, "S"));
            }
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
        private void updateAcitvity()
        {
            string activityType = "";
            string activeUser = user.Split('|')[1];
            if (lblTitle.Content.ToString().Contains("EDIT"))
            {
                activityType = "updated";
            }
            else
            {
                activityType = "added";
                activities = new Activities()
                {
                    Activity = string.Format("{0} new Outgoing document", activityType),
                    User = activeUser,
                    DateTime = DateTime.Now
                };
                activities.addActivities();
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
                    updateAcitvity();
                    showInfo("Successfully Saved!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    //showError("An error has occured" + ex.Message);
                    this.Close();
                }
            }
        }
        private void buttonShowPDF(object sender, RoutedEventArgs e)
        {
            var at = sender as AccessText;

            if (!at.Text.Contains("...") && !at.Tag.ToString().Contains("..."))
            {
                var pdfView = new dPDFView(at.Tag.ToString());
                pdfView.Show();
            }
            else if (at.Tag.ToString().Contains("..."))
            {
                var pdfView = new dPDFView(at.Text.ToString());
                pdfView.Show();
            }
        }

    }
}
