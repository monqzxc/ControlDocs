using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class Routes
    {        
        public int DocDataID { get; set; }
        public string FocalName { get; set; }
        public string OriginOffice { get; set; }
        public string AddresseeName { get; set; }
        public string AddresseeOffice { get; set; }
        

        public string Focal()
        {
            string a= "";

            var db = new dbDocs();
            


            return a;
        }
    }
}
