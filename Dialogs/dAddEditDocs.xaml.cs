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
        public dAddEditDocs()
        {
            InitializeComponent();
        }

        public dAddEditDocs(string dialogName, int id)
        {
            InitializeComponent();
            lblTitle.Content = dialogName;

            var focals = db.Focals.Select(x => x.FullName).ToList();
            ComboBox(cmbFocals, focals);
          
        }
        dbDocs db = new dbDocs();
        Addressee addressee = new Addressee();
        DocData docData = new DocData();
        DocPath docPath = new DocPath();
        private string currentFileName;
        private string destinationPath;
        private DateTime? signedDate;
        private DateTime? receivedDate;        
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
        

        private void browseFile()
        {

            //open dialog tool to open a file then store the filename into label
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Desktop";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentFileName = openFileDialog.FileName;                
            }
            else
            {
                return;
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
                File.Copy(receivedCopy, destinationDirectory + System.IO.Path.GetFileName(receivedCopy));
                destinationPath = destinationDirectory + System.IO.Path.GetFileName(receivedCopy);
                docPath.pathList.Add(destinationPath);
            }
            if (!btnSigned.Content.ToString().Contains("ADD"))
            {
                File.Copy(signedCopy, destinationDirectory + System.IO.Path.GetFileName(signedCopy));
                destinationPath = destinationDirectory + System.IO.Path.GetFileName(signedCopy);
                docPath.pathList.Add(destinationPath);
            }                       
        }
        private void btnSigned_Click(object sender, RoutedEventArgs e)
        {
            changeButtonContent(btnSigned, "ADD SIGNED COPY", "REMOVE", dpSigned, tpSigned, lblSigned);           
            //Console.WriteLine(DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(dpSigned.Text).ToString("yyyy-MM-dd"), DateTime.Parse(tpSigned.Text).ToString("HH:mm:ss"))));
        }
        private void btnReceived_Click(object sender, RoutedEventArgs e)
        {
            changeButtonContent(btnReceived, "ADD RECEIVED COPY", "REMOVE", dpReceived, tpReceived, lblReceived);
        }
        private void changeButtonContent(Button btn, string currentContent, string newcontent, DatePicker dp, TimePicker tp, Label lbl)
        {
            if (btn.Content.ToString().Contains("ADD"))
            {
                browseFile();
                btn.Content = newcontent;
                dp.SelectedDate = DateTime.Now;
                tp.SelectedTime = DateTime.Now;
                lbl.Content = currentFileName;
                btn.Background = Brushes.OrangeRed;
            }
            else
            {
                btn.Content = currentContent;
                dp.SelectedDate = null;
                tp.SelectedTime = null;
                lbl.Content = "...";
                btn.Background = Brushes.Teal;
            }
        }
        private void bindData()
        {
            //this method is to store data to corresponding models
            addressee = new Addressee()
            {
                ContactNo = txtContact.Text,
                Email = txtEmail.Text,
                FullName = txtFullName.Text,
                Office = txtOffice.Text
            };

            if (btnSigned.Content.ToString().Contains("ADD"))
                signedDate = null;
            else
                signedDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(dpSigned.Text).ToString("yyyy-MM-dd"), DateTime.Parse(tpSigned.Text).ToString("HH:mm:ss")));

            Console.WriteLine(signedDate);

            if (btnReceived.Content.ToString().Contains("ADD"))
                receivedDate = null;
            else
                receivedDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(dpReceived.Text).ToString("yyyy-MM-dd"), DateTime.Parse(tpReceived.Text).ToString("HH:mm:ss")));


            docData = new DocData()
            {
                DocSubject = txtSubject.Text,
                CurrentStatus = cmbStatus.Text,
                ForSigned = DateTime.Now,
                Signed = signedDate,
                ForRelease = receivedDate,
                FocalID = db.Focals.Where(x => x.FullName.Equals(cmbFocals.Text)).FirstOrDefault().Id,
                DateAdd = DateTime.Now,
                DoctTypes = txtDocType.Text,
                AddresseeID = db.Addressees.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault(),
                Remarks = txtRemarks.Text,
                Tag = "O",
                DocControlNumber = txtControlNumber.Text              
            };

            docPath.DocDataID = db.DocDatas.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
         
        }
        private void addDoc()
        {
            //calling method from models
            addressee.addAddressee();                       
            docData.addDocData();
            uploadFile();            
            docPath.addPath();
        }
        private void editDoc()
        {
            //calling method from models
            addressee.editAddressee();
            docData.editDocData();
        }
        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!isValid())
                return;
            else
            {
                try
                {
                    bindData();
                    if (lblTitle.Content.ToString().Contains("ADD"))
                        addDoc();
                    else
                        editDoc();

                    showInfo("Success bitch");
                    this.Close();
                }
                catch (Exception ex)
                {
                    showError("An error has occured" + ex.Message);
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
