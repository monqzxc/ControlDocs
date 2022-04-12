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
    /// Interaction logic for dAddFocalship.xaml
    /// </summary>
    public partial class dAddFocalship : Window
    {
        public dAddFocalship(string user, int id, string title)
        {
            InitializeComponent();
            this.role = int.Parse(user.Split('|')[0]);
            this.nickname = user.Split('|')[1];
            this.user = user;

            this.focalshipID = id;
            lblTitle.Content = title;
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
        int focalshipID;
        dbDocs db = new dbDocs();
        public ObservableCollection<Focalship> Focalship
        {
            get
            {
                var focalshipList = new ObservableCollection<Focalship>();
                var f = db.Focalships.Where(x => x.Id.Equals(focalshipID));
                foreach (var item in f)
                {
                    focalshipList.Add(new Focalship()
                    {
                        Acronym = item.Acronym,
                        FocalshipName = item.FocalshipName
                    });
                }
                return focalshipList;
            }
        }

        bool isValid()
        {
            if (string.IsNullOrWhiteSpace(txtAcronym.Text) ||
                string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                showError("Please fill-out all fields before saving");
                return false;
            }
            return true;
        }

        private void updateDatabase()
        {
            var p = new Focalship()
            {
                Id = focalshipID,
                Acronym = txtAcronym.Text,
                FocalshipName = txtPosition.Text
            };
            if (lblTitle.Content.ToString().Contains("EDIT"))
                p.editFocalship();
            else
                p.addFocalship();
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
                    Activity = string.Format("{0} focalship", activityType),
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!isValid())
                return;
            updateDatabase();
            updateAcitvity();
            showInfo("Successfully Saved!");
            this.Close();
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
