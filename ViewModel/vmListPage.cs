using DocsControl.Dialogs;
using DocsControl.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static DocsControl.Model.Modules;

namespace DocsControl.ViewModel
{
   public partial class vmListPage
    {
        public vmListPage(string title, string user)
        {
            InitializeComponent();
            lblTitle.Content = title;

            this.role = int.Parse(user.Split('|')[0]);
            this.nickname = user.Split('|')[1];
            this.user = user;
            this.DataContext = this;
            if (role > 1)
            {
                btnAdd.Visibility = Visibility.Hidden;
            }

        }
        private string user;
        private string nickname;
        private int role;
        IEnumerable<Directories> data;
        private string title;
        private int listID;
        public ObservableCollection<Directories> NewDirectories
        {
            get
            {
                var directories = new ObservableCollection<Directories>();
               
                var db = new dbDocs();

                //load collection of data depends on page
                switch (lblTitle.Content)
                {
                    case "USERS":
                        data = db.Users.Select(x => new Directories
                        {
                            User = x,
                            Id = x.Id,
                            Name = string.Concat(x.FirstName, " ", x.LastName),
                            Description = x.Role.RoleName
                        }).ToList();

                        dgDocList.Columns[0].Header = "FULL NAME";
                        dgDocList.Columns[1].Header = "ROLE";

                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cmbName, "FULL NAME");
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cmbItem, "ROLE");
                       
                        break;

                    case "FOCALS":
                        data = db.Focals.Select(x => new Directories
                        {
                            Focal = x,
                            Id = x.Id,
                            Name = x.FullName,
                            Description = x.Office.OperatingUnit
                        }).ToList();

                        dgDocList.Columns[0].Header = "FULL NAME";
                        dgDocList.Columns[1].Header = "OFFICE";

                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cmbName, "FULL NAME");
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cmbItem, "OFFICE");
                        
                        break;

                    case "FOCALSHIP":
                        data = db.Focalships.Select(x => new Directories
                        {
                            Focalship = x,
                            Id = x.Id,
                            Name = x.Acronym,
                            Description = x.FocalshipName
                        }).ToList();

                        dgDocList.Columns[0].Header = "ACRONYM";
                        dgDocList.Columns[1].Header = "TITLE";

                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cmbName, "ACRONYM");
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cmbItem, "TITLE");

                        break;

                    case "POSITIONS":
                        data = db.Plantillas.Select(x => new Directories
                        {
                            Plantilla = x,
                            Id = x.Id,
                            Name = x.Acronym,
                            Description = x.Item
                        }).ToList();

                        dgDocList.Columns[0].Header = "ACRONYM";
                        dgDocList.Columns[1].Header = "POSITION";

                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cmbName, "ACRONYM");
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cmbItem, "POSITION");

                        break;
                }

                //adding of data into model
                foreach (var item in data)
                {
                    directories.Add(new Directories()
                    { 
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }

                //getting distinct values for combobox
                foreach (var item in sp2.Children)
                {
                    if (item.GetType() == typeof(ComboBox))
                    {
                        var cmb = item as ComboBox;
                        if (cmb.Name.Contains("Item"))
                        {
                            cmb.ItemsSource = data.Select(x => x.Description).Distinct();
                        }
                    }
                }
                return directories;
            }
        }
        private void customTitle(Button item, string title1, string title2)
        {
           
            if (item.Content.Equals("VIEW"))
            {
                title =  title1;
                listID = int.Parse(item.Tag.ToString());
            }
            else
            {
                title = title2;
                listID = 0;
            }
        }
        private void buttonShowDialog(object sender, RoutedEventArgs e) //event for clicking add button
        {
            var item = sender as Button;

            this.Opacity = .6;
            this.Background = Brushes.Black;

            switch (lblTitle.Content)
            {
                case "USERS":
                    customTitle(item, "EDIT USER", "ADD USER");
                    var u = new dAddUsers(user,listID , title);
                    u.ShowDialog();
                    break;
                case "FOCALS":
                    var f = new dAddFocals(user);
                    f.ShowDialog();
                    break;
                case "FOCALSHIP":
                    var fs = new dAddFocalship(user);
                    fs.ShowDialog();
                    break;
                case "POSITIONS":
                    var p = new dAddPlantilla(user);
                    p.ShowDialog();
                    break;

            }
            
            this.Opacity = 1;
            this.Background = Brushes.Transparent;
        }
    }
}
