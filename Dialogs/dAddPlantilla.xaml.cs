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
    /// Interaction logic for dAddPlantilla.xaml
    /// </summary>
    public partial class dAddPlantilla : Window
    {
        public dAddPlantilla(string user, int id, string title)
        {
            InitializeComponent();
            this.role = int.Parse(user.Split('|')[0]);
            this.nickname = user.Split('|')[1];
            this.user = user;

            this.plantillaID = id;
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
        private int plantillaID;
        dbDocs db = new dbDocs();

        public ObservableCollection<Plantilla> Plantilla
        {
            get
            {
                var plantillaList = new ObservableCollection<Plantilla>();
                var p = db.Plantillas.Where(x => x.Id.Equals(plantillaID));
                foreach (var item in p)
                {
                    plantillaList.Add(new Plantilla()
                    {
                        Acronym = item.Acronym,
                        Item = item.Item
                    });
                }
                return plantillaList;
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
            var p = new Plantilla()
            {
                Id = plantillaID,
                Acronym = txtAcronym.Text,
                Item = txtPosition.Text
            };
            if (lblTitle.Content.ToString().Contains("EDIT"))
                p.editPlantilla();
            else
                p.addPlanntilla();
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
                    Activity = string.Format("{0} position", activityType),
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
