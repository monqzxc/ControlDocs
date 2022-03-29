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
        public dAddEditDocs2(string title, int id)
        {
            InitializeComponent();
            this.DataContext = this;
            btnRemoveDoc.Visibility = Visibility.Hidden;
            checkList.Clear();
            this.docDataID = id;
            //continuation of designing then finish the functionality
        }

        dbDocs db = new dbDocs();
        private string currentFileName;
        Addressee addressee = new Addressee();
        DocData docData = new DocData();
        DocPath docPath = new DocPath();
        private int docDataID;
        public ObservableCollection<NewCheckBox> CheckBoxes
        {
            get
            {
                var focal = db.Focals.ToList();
                var focalList = new ObservableCollection<NewCheckBox>();
                foreach (var item in focal)
                {
                    var chk = new NewCheckBox
                    {
                        Content = item.NickName,
                        Tag = item.Id.ToString(),                   
                    };
                    focalList.Add(chk);
                }
                return focalList;
            }
        }
        private void chkClickEvent(object sender, RoutedEventArgs e)
        {
            var item = sender as System.Windows.Controls.CheckBox;
            Console.WriteLine(item.Tag.ToString());
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

            //saving validation of signed copy
            //if (cmbStatus.Text.Contains("SIGNED") && (btnSigned.Content.ToString().Contains("ADD") || string.IsNullOrWhiteSpace(dpSigned.Text) || string.IsNullOrWhiteSpace(tpSigned.Text)))
            //{
            //    showError("Save Failed: Please input date and time of Signed Copy");
            //    return false;
            //}

            ////saving validation of received copy
            //if (cmbStatus.Text.Contains("RECEIVED") && (btnReceived.Content.ToString().Contains("ADD") || string.IsNullOrWhiteSpace(dpReceived.Text) || string.IsNullOrWhiteSpace(tpReceived.Text)))
            //{
            //    showError("Save Failed: Please input date and time of Received Copy");
            //    return false;
            //}

            return true;
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


      
        private void btnRemoveDoc_Click(object sender, RoutedEventArgs e)
        {
            if (lblPath.Text.Contains(@"\"))
            {
                btnRemoveDoc.Visibility = Visibility.Hidden;
                btnViewDocFile.Content = "BROWSE";
                btnViewDocFile.Margin = new Thickness(0, -106, 0, 0);
                lblPath.Text = "";
            }// add removing in the database and path here....



         //   foreach (var item in checkList)
         //   {
         //       Console.WriteLine(item);
         //   }
         ////   Console.WriteLine(checkList.Count);
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

                btnRemoveDoc.Visibility = Visibility.Visible;
            }
            else
            {
                var pdfView = new dPDFView(lblPath.Text);
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
                FocalID = string.Join(",", checkList), //customized focal ID, some of them has multiple shit tanginaaaaaaaaa
                DateAdd = receievedDate,
                DoctTypes = txtDocType.Text,
                AddresseeID = lblTitle.Content.ToString().Contains("ADD") ? db.Addressees.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() : int.Parse(txtAddresseeOffice.Tag.ToString()),
                Remarks = string.Format("{0},{1}", txtRemarks1.Text,txtRemarks2.Text),
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
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(string.Join(",", checkList));
            if (!isValid())
                return;
            else
            {
                //try
                //{
                updateDatabase();
                showInfo("Successfully Saved!");
                this.Close();
                //}
                //catch (Exception ex)
                //{
                //    //showError("An error has occured" + ex.Message);
                //    this.Close();
                //}
            }
        }
    }
}
