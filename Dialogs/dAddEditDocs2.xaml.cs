using DocsControl.Model;
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
    /// Interaction logic for dAddEditDocs2.xaml
    /// </summary>
    public partial class dAddEditDocs2 : Window
    {
        public dAddEditDocs2(string title, int id, string user)
        {
            InitializeComponent();
            lblTitle.Content = title;
            
            btnRemoveDoc.Visibility = Visibility.Hidden;
            
            checkList.Clear();
            this.docDataID = id;

            this.user = user;
            this.nickName = user.Split('|')[1];
            this.role = int.Parse(user.Split('|')[0]);

            if (role > 2)
            {
                txtSubject.IsEnabled = false;
                txtNumberORD.IsEnabled = false;
                txtNumberROD.IsEnabled = false;
                txtDocType.IsEnabled = false;
                txtNumberDoc.IsEnabled = false;
                dpDateOfDocFS.IsEnabled = false;
                txtAddresseeFullName.IsEnabled = false;
                txtAddresseeOffice.IsEnabled = false;
                icPersonnel.IsEnabled = false;
                txtRemarks1.IsEnabled = false;
                dpRecievedByFocalS.IsEnabled = false;
                dpAddedReceived.IsEnabled = false;
                tpAddedReceived.IsEnabled = false;
                btnRemoveDoc.IsEnabled = false;                
            }
            DataContext = new
            {
                checkBoxes = CheckBoxes,
                docDatas = doctDatas
            };
            if (btnViewDocFile.Content.ToString().Contains("VIEW"))
                btnViewDocFile.Margin = new Thickness(0, -85, 0, 0);
   
           
        }

        dbDocs db = new dbDocs();
        private string currentFileName;
        Addressee addressee = new Addressee();
        DocData docData = new DocData();
        DocPath docPath = new DocPath();
        Activities activities = new Activities();
        private int docDataID;
        private string user;
        private string nickName;
        private int role;
        public List<NewCheckBox> CheckBoxes
        {
            get
            {
                checkList.Clear();
                var focalList = new List<NewCheckBox>();
                bool isChecked = false; //checked
                var focal = db.Focals.OrderBy(x => x.PlantillaID).ToList();               
                var dbFocalList = new List<string>(); //focal list from database based on
                if (lblTitle.Content.ToString().Contains("EDIT"))
                {
                    var focalIDList = db.DocDatas.Where(x => x.Id.Equals(docDataID)).Select(x => x.FocalID).FirstOrDefault().Split(',').ToList(); //get focals id then put them into list

                    foreach (var item in focalIDList) //getting all the id in the previous list then get their nickname column before adding into the list
                    {
                        int id = int.Parse(item);
                        dbFocalList.Add(db.Focals.Where(x => x.Id.Equals(id)).Select(x => x.NickName).FirstOrDefault()); 
                    }
                  
                }

                foreach (var item in focal)
                {
                    if (dbFocalList.Contains(item.NickName))
                        isChecked = true;
                    else
                        isChecked = false;

                    var chk = new NewCheckBox
                    {
                        Content = item.NickName,
                        Tag = item.Id.ToString(),     
                        IsChecked = isChecked
                    };
                    focalList.Add(chk);
                }
                return focalList;
            }
        }

        public ObservableCollection<IncomingClass> doctDatas
        {
            get
            {
                var doc = new DocData();
                doc.Id = docDataID;
                
                var doctList = new ObservableCollection<IncomingClass>();
                foreach (var item in doc.GetDocDatas())
                {
                   
                    doctList.Add(new IncomingClass()
                    {
                        DocDataID = item.Id,
                        DocSubject = item.DocSubject,
                        Status = item.CurrentStatus,
                        DocumentType = item.DoctTypes,
                        ORDNumber = item.DocControlNumber.Split('|')[0],
                        RODNumber = item.DocControlNumber.Split('|')[1],
                        DocNumber = item.DocControlNumber.Split('|')[2],
                        Tag = item.Tag,
                        DateAdded = item.DateAdd,
                        DateOfDocument = item.ForSigned,
                        DateReceivedByFocal = item.Signed,
                        ActionDate = item.ForRelease,
                        Remarks1 = item.Remarks.Split('|')[0],
                        Remarks2 = item.Remarks.Split('|')[1],
                        FilePath = item.DocPaths.Select(x => x.Path).FirstOrDefault(),
                        OriginID = item.Addressee.Id,
                        OriginOffice = item.Addressee.Office,
                        OriginSignatory = item.Addressee.FullName,
                        Focals = CheckBoxes
                    });
               
                }
                if (lblTitle.Content.ToString().Contains("ADD")) //add checkboxes
                {
                    doctList.Add(new IncomingClass()
                    {
                        Focals = CheckBoxes,
                        DateAdded = DateTime.Now,
                    });
                }

                //this code will load the file path
                docPath.DocDataID = docDataID;
                string receivedCopyFile = "";
                string receivedPathFile = "";
                if (docPath.GetDocPaths("R").Count() > 0) //get file name and trim (if there's any)
                {
                    receivedCopyFile = new FileInfo(docPath.GetDocPaths("R").FirstOrDefault().Path).Name;
                    receivedPathFile = docPath.GetDocPaths("R").FirstOrDefault().Path;
                }
                else
                    receivedCopyFile = "";

               // bind file name(if there's any)
                lblPath.Text = lblPath.Text.Contains("ADD") ? "" : receivedCopyFile;
                lblPath.Tag = lblPath.Text.Contains("ADD") ? "" : receivedPathFile;

                //change of content and color in buttons
                if (!lblPath.Text.Equals(""))
                {
                    btnViewDocFile.Content = "VIEW";
                    btnRemoveDoc.Visibility = Visibility.Visible;
                }                

                //return docdata
                return doctList;
            }
        }        
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton.Equals(MouseButton.Left))
                DragMove();
        }
        bool isValid() //validation of inputs before saving. skip if no errors
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text) ||
                string.IsNullOrWhiteSpace(txtNumberORD.Text) ||
                string.IsNullOrWhiteSpace(txtNumberROD.Text) ||
                string.IsNullOrWhiteSpace(txtDocType.Text) ||
                string.IsNullOrWhiteSpace(dpDateOfDocFS.Text) ||
                string.IsNullOrWhiteSpace(txtNumberDoc.Text) ||
                string.IsNullOrWhiteSpace(txtAddresseeOffice.Text) ||
                string.IsNullOrWhiteSpace(txtAddresseeFullName.Text) ||
                string.IsNullOrWhiteSpace(txtRemarks1.Text) ||
                string.IsNullOrWhiteSpace(dpRecievedByFocalS.Text) ||
                checkList.Count <=0 ||
                string.IsNullOrWhiteSpace(dpAddedReceived.Text) ||
                string.IsNullOrWhiteSpace(tpAddedReceived.Text))
            {
                showError("Please fill out all necessary fields (*) before saving the document");
                return false;
            }
           
            return true;
        }
       
        private bool browseFile()
        {
            //open dialog tool to open a file then store the filename into label
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Desktop";
            openFileDialog.Filter = "PDF Files | *.pdf";
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
      
        private void btnRemoveDoc_Click(object sender, RoutedEventArgs e)
        {
            if (lblPath.Text.Contains(@"\"))
            {
                btnRemoveDoc.Visibility = Visibility.Hidden;
                btnViewDocFile.Content = "BROWSE";
                btnViewDocFile.Margin = new Thickness(0, -106, 0, 0);
                lblPath.Text = "";
            }// add removing in the database and path here....
            if (lblTitle.Content.ToString().Contains("EDIT"))
            {              

                if (lblPath.Tag.ToString().Contains("R4A_FileServer")) //if the file is already in database. return true
                {
                    if (showWarning("DO YOU WANT TO REMOVE THIS FILE?").Equals(true))
                    {
                        //store the path in the list to be deleted/removed. right after removing the file. change the button properties
                        var itemPath = docPath.GetDocPaths("R").FirstOrDefault().Path;
                        docPath.pathItem.Add(itemPath);
                        btnViewDocFile.Content = "BROWSE";
                        btnViewDocFile.Margin = new Thickness(0, -106, 0, 0);
                        btnRemoveDoc.Visibility = Visibility.Hidden;
                        lblPath.Text = "";
                    }
                    else
                        return;
                }
                btnRemoveDoc.Visibility = Visibility.Hidden;
                btnViewDocFile.Content = "BROWSE";
                btnViewDocFile.Margin = new Thickness(0, -106, 0, 0);
                lblPath.Text = "";
            }
            
        }

        private void btnViewDocFile_Click(object sender, RoutedEventArgs e)
        {
            if (btnViewDocFile.Content.ToString().Contains("BROWSE"))
            {
                if (!browseFile())
                    return;
                btnViewDocFile.Content = "VIEW";
                btnViewDocFile.Margin = new Thickness(0, -85, 0, 0);
                lblPath.Text = currentFileName;
                lblPath.Tag = currentFileName;
                btnRemoveDoc.Visibility = Visibility.Visible;
            }
            else
            {
                string path = "";
                if (lblTitle.Content.ToString().Contains("EDIT"))
                    path = lblPath.Tag.ToString();
                else
                    path = lblPath.Text;
                
                var pdfView = new dPDFView(path);
                pdfView.Show();
            }
        }

        private void bindAddressee()
        {
            addressee = new Addressee()
            {                              
                FullName = txtAddresseeFullName.Text,
                Office = txtAddresseeOffice.Text,
                Id = lblTitle.Content.ToString().Contains("ADD") ? 0 : int.Parse(txtAddresseeOffice.Tag.ToString())
            };
        }
        private void bindDocData()
        {
            DateTime? actionDate = null;
            if (!string.IsNullOrEmpty(dpDateOfActionR.Text))
            {
                actionDate =
                new DateTime(
                    dpDateOfActionR.SelectedDate.Value.Year,
                    dpDateOfActionR.SelectedDate.Value.Month,
                    dpDateOfActionR.SelectedDate.Value.Day,
                    tpDateOfActionR.SelectedTime.Value.Hour,
                    tpDateOfActionR.SelectedTime.Value.Minute,
                    tpDateOfActionR.SelectedTime.Value.Second);
            }

            DateTime receievedDate =
                new DateTime(
                    dpAddedReceived.SelectedDate.Value.Year,
                    dpAddedReceived.SelectedDate.Value.Month,
                    dpAddedReceived.SelectedDate.Value.Day,
                    tpAddedReceived.SelectedTime.Value.Hour,
                    tpAddedReceived.SelectedTime.Value.Minute,
                    tpAddedReceived.SelectedTime.Value.Second);

            docData = new DocData()
            {
                Id = docDataID,
                DocSubject = txtSubject.Text,
                CurrentStatus = txtStatus.Text,
                ForSigned = dpDateOfDocFS.SelectedDate,
                Signed = dpRecievedByFocalS.SelectedDate,
                ForRelease = actionDate,
                FocalID = string.Join(",", checkList.Distinct().OrderBy(x => int.Parse(x))), //customized focal ID, then sort
                DateAdd = receievedDate,
                DoctTypes = txtDocType.Text,
                AddresseeID = lblTitle.Content.ToString().Contains("ADD") ? db.Addressees.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() : int.Parse(txtAddresseeOffice.Tag.ToString()),
                Remarks = string.Format("{0}|{1}", txtRemarks1.Text,txtRemarks2.Text),
                Tag = "I",  
                DocControlNumber = string.Format("{0}|{1}|{2}", txtNumberORD.Text, txtNumberROD.Text,txtNumberDoc.Text) //when getting values. the sequence is ORD > ROD > DocNumber from file
            };
        }


        private void uploadFile()
        {
            //getting the current path
            string filePath = lblPath.Text;        
            //setting the destination path
            string destinationDirectory = @"\\R4A_FileServer\Control of Documents\";

            //combine current and destination path then copy            
            if (btnViewDocFile.Content.ToString().Contains("VIEW") && lblPath.Text.Contains(@"\"))
            {
                var f = new FileInfo(filePath);
                var fileName = destinationDirectory + string.Format("{0}-{1}", "RECEIVED" + DateTime.Now.ToString("yyyyMMddHHmm"), f.Name);
                File.Copy(filePath, fileName, true);//copy and renaming of file               

                docPath.pathList.Add(string.Format("{0}|{1}", fileName, "R"));//add to list 
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
                    Activity = string.Format("{0} new Incoming document", activityType),
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
                    showError("An error has occured" + ex.Message);
                    this.Close();
                }
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (showWarning("DO YOU WANT TO CANCEL?").Equals(true))
            {
                //store the path in the list to be deleted/removed
                this.Close();
            }
            else
                return;

        }
    }
}
