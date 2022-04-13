using DocsControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static DocsControl.Model.Modules;


namespace DocsControl.ViewModel
{
    public partial class vmDashboard : UserControl
    {
        public vmDashboard(string Nickname)
        {
            InitializeComponent();
            lblWelcome.Content = string.Format("WELCOME {0}!", Nickname.Split('|')[1]);
            this.DataContext = this;
        }
        dbDocs db = new dbDocs();


        public ObservableCollection<DashboardData> DashboardDatas
        {
            get
            {
                var d = new ObservableCollection<DashboardData>();
                
                return d;
            }
        }
    }
}
