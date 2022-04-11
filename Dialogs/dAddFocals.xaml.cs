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
using static DocsControl.Model.Modules;

namespace DocsControl.Dialogs
{
    /// <summary>
    /// Interaction logic for dAddFocals.xaml
    /// </summary>
    public partial class dAddFocals : Window
    {
        public dAddFocals(string user, int id, string title)
        {
            InitializeComponent();
            this.role = int.Parse(user.Split('|')[0]);
            this.nickname = user.Split('|')[1];
            this.user = user;
            this.focalID = id;
            lblTitle.Content = title;
            loadComboBox();
            this.DataContext = this;

            if (role > 1)
            {
                sp1.IsEnabled = false;
                btnSave.IsEnabled = false;
            }

        }

        private string user;
        private string nickname;
        private int role;
        private int focalID;
        dbDocs db = new dbDocs();

        private void loadComboBox()
        {
            var office = db.Offices.Select(x => x.OperatingUnit).ToList();
            var focalship = db.Focalships.Select(x => x.FocalshipName).ToList();
            var plantillia = db.Plantillas.Select(x => x.Position).ToList();

            ComboBox(cmbOffice, office);
            ComboBox(cmbFocalship, focalship);
            ComboBox(cmbPosition, plantillia);
        }
        public ObservableCollection<Focal> Focal
        {
            get
            {
                var focalList = new ObservableCollection<Focal>();
                var f = db.Focals.Where(x => x.Id.Equals(focalID));
                foreach (var item in f)
                {
                    focalList.Add(new Focal()
                    {
                        FullName = item.FullName,
                        NickName = item.NickName,
                        Email = item.Email,
                        ContactNumber = item.ContactNumber,
                        FocalshipID = item.FocalshipID - 1,
                        OfficeID = item.OfficeID - 1,
                        PlantillaID = item.PlantillaID - 1
                    });
                }
                return focalList;
            }
        }

        bool isValid()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtNickname.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(cmbFocalship.Text) ||
                string.IsNullOrWhiteSpace(cmbOffice.Text) ||
                string.IsNullOrWhiteSpace(cmbPosition.Text))
            {
                showError("Please fill-out all fields before saving");
                return false;
            }
            return true;
        }

        private void updateDatabase()
        {
            var officeID = db.Offices.Where(x => x.OperatingUnit.Equals(cmbOffice.Text)).FirstOrDefault().Id;
            var focalshipID = db.Focalships.Where(x => x.FocalshipName.Equals(cmbFocalship.Text)).FirstOrDefault().Id;
            var plantillaID = db.Plantillas.Where(x => x.Position.Equals(cmbPosition.Text)).FirstOrDefault().Id;
            var focal = new Focal()
            {
                Id = focalID,
                FullName = txtFullName.Text,
                NickName = txtNickname.Text,
                ContactNumber = txtContact.Text,
                Email = txtEmail.Text,
                FocalshipID = focalshipID,
                PlantillaID = plantillaID,
                OfficeID = officeID
            };
            if (lblTitle.Content.ToString().Contains("EDIT"))
                focal.editFocal();
            else
                focal.addFocal();
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
                activityType = "added new";
                var activities = new Activities()
                {
                    Activity = string.Format("{0} focal", activityType),
                    User = activeUser,
                    DateTime = DateTime.Now
                };
                activities.addActivities();
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton.Equals(MouseButton.Left))
                DragMove();
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


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!isValid())
                return;
            updateDatabase();
            updateAcitvity();
            showInfo("Successfully Saved!");
            this.Close();
        }
    }
}
