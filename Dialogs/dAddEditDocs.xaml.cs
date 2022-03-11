using DocsControl.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
        public dAddEditDocs(string dialogName, int id)
        {
            InitializeComponent();
            lblTitle.Content = dialogName;
            this.docDataID = id;
            docData.Id = id;

            var focals = db.Focals.Select(x => x.FullName).ToList();
            ComboBox(cmbFocals, focals);

            if (dialogName.Contains("EDIT"))
            {
                loadInfo();
               
                addresseeID = docData.GetDocDatas().FirstOrDefault().AddresseeID;
            }

            }
        dbDocs db = new dbDocs();
        Addressee addressee = new Addressee();
        DocData docData = new DocData();
        DocPath docPath = new DocPath();
        private int docDataID;
        private string currentFileName;
        private string destinationPath;
        private DateTime? signedDate;
        private DateTime? receivedDate;
        private int addresseeID;
        
        bool isValid()
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

            if(cmbStatus.Text.Contains("SIGNED") && (btnSigned.Content.ToString().Contains("ADD") || string.IsNullOrWhiteSpace(dpSigned.Text) || string.IsNullOrWhiteSpace(tpSigned.Text)))
            {
                showError("Save Failed: Please input date and time of Signed Copy");
                return false;
            }
            if (cmbStatus.Text.Contains("RELEASED") && (btnReceived.Content.ToString().Contains("ADD") || string.IsNullOrWhiteSpace(dpReceived.Text) || string.IsNullOrWhiteSpace(tpReceived.Text)))
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
            //getting the current path
            string signedCopy = lblSigned.Content.ToString();
            string receivedCopy = lblReceived.Content.ToString();
            //setting the destination path
            string destinationDirectory = @"\\R4A_FileServer\Control of Documents\";

            //combine current and destination path then copy            
            if (!btnReceived.Content.ToString().Contains("ADD"))
            {
                var f = new FileInfo(receivedCopy);
                File.Copy(receivedCopy, destinationDirectory + string.Format("{0}-{1}", "RECEIVED" + DateTime.Now.ToString("yyyyMMddHHmmss"), f.Name),true);//copy and renaming of file
                destinationPath = destinationDirectory +string.Format("{0}-{1}", "RECEIVED" + DateTime.Now.ToString("yyyyMMddHHmmss"), f.Name);//storing the path to database
                docPath.pathList.Add(string.Format("{0}|{1}",destinationPath,"R"));//add to list 
            }
            if (!btnSigned.Content.ToString().Contains("ADD"))
            {
                var f = new FileInfo(signedCopy);
                File.Copy(signedCopy, destinationDirectory + string.Format("{0}-{1}", "SIGNED" + DateTime.Now.ToString("yyyyMMddHHmmss"), f.Name),true);
                destinationPath = destinationDirectory + string.Format("{0}-{1}", "SIGNED" + DateTime.Now.ToString("yyyyMMddHHmmss"), f.Name);
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
            
            if (btn.Content.ToString().Contains("ADD"))
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
                            docPath.Id = docPath.GetDocPaths(lbl.Content.ToString()[0].ToString()).FirstOrDefault().Id;
                            deletePath(docPath.GetDocPaths(lbl.Content.ToString()[0].ToString()).FirstOrDefault().Path);
                            docPath.deletePath();                            
                            bindDocData();
                            docData.editDocData();
                            showInfo("hatdog");               
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
                Id = addresseeID
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
                DateAdd = DateTime.Now,
                DoctTypes = txtDocType.Text,
                AddresseeID = lblTitle.Content.ToString().Contains("ADD") ? db.Addressees.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() : addresseeID,
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

            uploadFile();

            docPath.DocDataID = db.DocDatas.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();

            if (lblTitle.Content.ToString().Contains("ADD"))
                docPath.addPath();
            //else
            //    docPath.ed();
        }   

        private void loadInfo()
        {
            //bind data to textfields and combo boxes
            txtSubject.Text = docData.GetDocDatas().FirstOrDefault().DocSubject;
            cmbStatus.Text = docData.GetDocDatas().FirstOrDefault().CurrentStatus;
            txtDocType.Text = docData.GetDocDatas().FirstOrDefault().DoctTypes;
            cmbFocals.Text = docData.GetDocDatas().FirstOrDefault().Focal.FullName;
            txtControlNumber.Text = docData.GetDocDatas().FirstOrDefault().DocControlNumber;
            txtRemarks.Text = docData.GetDocDatas().FirstOrDefault().Remarks;
            txtOffice.Text = docData.GetDocDatas().FirstOrDefault().Addressee.Office;
            txtFullName.Text = docData.GetDocDatas().FirstOrDefault().Addressee.FullName;
            txtEmail.Text = docData.GetDocDatas().FirstOrDefault().Addressee.Email;
            txtContact.Text = docData.GetDocDatas().FirstOrDefault().Addressee.ContactNo;

            dpSigned.SelectedDate = docData.GetDocDatas().FirstOrDefault().Signed;
            dpReceived.SelectedDate = docData.GetDocDatas().FirstOrDefault().ForRelease;
            tpSigned.SelectedTime = docData.GetDocDatas().FirstOrDefault().Signed;
            tpReceived.SelectedTime = docData.GetDocDatas().FirstOrDefault().ForRelease;

            docPath.DocDataID = docData.Id;

            string signedCopyFile;
            string receivedCopyFile;
            if (docPath.GetDocPaths("S").Count() > 0)
                signedCopyFile = new FileInfo(docPath.GetDocPaths("S").FirstOrDefault().Path).Name;
            else
                signedCopyFile = "...";

            if (docPath.GetDocPaths("R").Count() > 0)
                receivedCopyFile = new FileInfo(docPath.GetDocPaths("R").FirstOrDefault().Path).Name;
            else
                receivedCopyFile = "...";


            lblSigned.Content = lblSigned.Content.ToString().Contains("ADD") ? "..." : signedCopyFile;
            lblReceived.Content = btnReceived.Content.ToString().Contains("ADD") ? "..." : receivedCopyFile;
            
            if (!string.IsNullOrWhiteSpace(dpSigned.Text) || !string.IsNullOrWhiteSpace(tpSigned.Text))            
            {
                btnSigned.Content = "REMOVE"; 
                btnSigned.Background = Brushes.OrangeRed; 
            }
            if (!string.IsNullOrWhiteSpace(dpReceived.Text) || !string.IsNullOrWhiteSpace(tpReceived.Text))
            {
                btnReceived.Content = "REMOVE";
                btnReceived.Background = Brushes.OrangeRed;
            }
        }       

        private void deletePath(string path)
        {
            System.IO.File.Delete(path);
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
                    showInfo("Success bitch");
                    this.Close();
                }
                catch (Exception ex)
                {
                    showError("An error has occured" + ex.Message);
                    return;
                }
            }
        }
        //private void btnBrowse_Copy_Click(object sender, RoutedEventArgs e)
        //{
        //    var pdfView = new dPDFView(lblPath.Content.ToString());
        //    pdfView.Show();
        //}
    }
}
