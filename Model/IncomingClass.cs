using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class IncomingClass
    {
        public int DocDataID { get; set; }
        public string DocSubject { get; set; }
        public string ORDNumber { get; set; }
        public string RODNumber { get; set; }
        public string DocumentType { get; set; }
        public string DocNumber { get; set; }
        public string OriginOffice { get; set; }
        public string OriginSignatory { get; set; }
        public DateTime? DateOfDocument { get; set; }
        public string Remarks1 { get; set; }
        public DateTime? DateReceivedByFocal { get; set; }
        public string Remarks2 { get; set; }
        public DateTime? ActionDate { get; set; }
        public string Status { get; set; }  
        public DateTime DateAdded { get; set; }
        public string Tag { get; set; }
        public string FilePath { get; set; }

        public List<NewCheckBox> Focals 
        {
            get
            {
                return _Focals;
            }
            set
            {
                _Focals = value;
            }
        }

        private List<NewCheckBox> _Focals = new List<NewCheckBox>();
        
    }
}
