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
    /// Interaction logic for dAddUsers.xaml
    /// </summary>
    public partial class dAddUsers : Window
    {
        public dAddUsers(string user, int id,string title)
        {
            InitializeComponent();
            this.role = int.Parse(user.Split('|')[0]);
            this.nickname = user.Split('|')[1];
            this.user = user;
            this.userId = id;
            lblTitle.Content = title;
            this.DataContext = this;

            if (role > 1)
            {
                sp1.IsEnabled = false;
                btnSave.IsEnabled = false;
            }
        }
        dbDocs db = new dbDocs();
        private string user;
        private string nickname;
        private int role;
        private int userId;
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton.Equals(MouseButton.Left))
                DragMove();
        }

        public ObservableCollection<User> Users
        {
            get
            {
             
                var userList = new ObservableCollection<User>();
                var u = db.Users.Where(x => x.Id.Equals(userId));
                foreach (var item in u)
                {
                    switch (item.Sex)
                    {
                        case "Male":
                            item.Sex = "0";
                            break;
                        case "Female":
                            item.Sex = "1";
                            break;
                    }

                    switch (item.RoleID)
                    {
                        case 1:
                            item.RoleID = 0;
                            break;
                        case 2:
                            item.RoleID = 1;
                            break;
                        case 3:
                            item.RoleID = 2;
                            break;
                    }

                    userList.Add(new User()
                    {
                        UserName = item.UserName,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Password = item.Password,
                        NickName = item.NickName,
                        RoleID = item.RoleID,
                        Sex = item.Sex
                    });
                }

                return userList;
            }
        }
       
        private void updateDatabase()
        {
            var user = new User()
            {
                Id = userId,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                NickName = txtNickName.Text,
                RoleID = cmbRole.SelectedIndex + 1,
                Sex = cmbSex.Text,
                UserName = txtUsername.Text,
                Password = txtPassword.Text
            };
            if (lblTitle.Content.ToString().Contains("EDIT"))
                user.editUser();
            else
                user.addUser(); 
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
                var activities = new Activities()
                {
                    Activity = string.Format("{0} new user", activityType),
                    User = activeUser,
                    DateTime = DateTime.Now
                };
                activities.addActivities();
            }
        }
        bool isValid()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtNickName.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(cmbRole.Text) ||
                string.IsNullOrWhiteSpace(cmbSex.Text))
            {
                showError("Please fill-out all details before saving");
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(cmbRole.SelectedIndex);
            if (!isValid())
                return;
            try
            {
                updateDatabase();
                updateAcitvity();
                showInfo("Successfully Saved!");
                this.Close();
            }
            catch (Exception ex)
            {

                showError(string.Format("{0} An error occur: ",ex.Message));
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
