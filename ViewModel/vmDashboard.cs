using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DocsControl.ViewModel
{
    public partial class vmDashboard : UserControl
    {
        public vmDashboard(string Nickname)
        {
            InitializeComponent();
            lblWelcome.Content = string.Format("WELCOME {0}!", Nickname.Split('|')[1]);
        }
    }
}
