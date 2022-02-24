using DocsControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static DocsControl.Model.DocData;

namespace DocsControl.ViewModel
{
    public partial class vmOutgoing : UserControl
    {
        public vmOutgoing()
        {         
            InitializeComponent();
            DocList();
        }
        
        public void DocList()
        {
            var db = new dbDocs();
            var offices = db.Offices.ToList();
            var officeList = new List<Office>();
            var sampleLilst = new string[] { 
                "1,Hatdog,For Sign,2022-10-29,-,-,Memo,1-,-",
                "2,DogHat,For Sign,2022-12-15,-,-,Letter,1-,-", 
                "3,GotHag,For Sign,2022-01-09,-,-,CTPR,1-,-", 
                "4,TogHad,For Sign,2022-07-31,-,-,Advisory,1-,-" };
            foreach (var item in offices)
            {
                officeList.Add(new Office() { OperatingUnit = item.OperatingUnit, Email = DateTime.Now.ToString("yyyy-MM-dd")} );
                lvForSign.ItemsSource = officeList;
            }
        }

       //public List<DocData> DocDatas
       // {
       //     get { return DocDataHelper.DocDatas; }
       // }
    }
}
