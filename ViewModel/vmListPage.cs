using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.ViewModel
{
   public partial class vmListPage
    {
        public vmListPage(string title)
        {
            InitializeComponent();
            lblTitle.Content = title;
        }
    }
}
