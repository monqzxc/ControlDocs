using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class DocPath
    {
        public int Id { get; set; }
  
        public string Path { get; set; }
        public int DocDataID { get; set; }
        
        public virtual DocData DocData { get; set; }
    }
}
