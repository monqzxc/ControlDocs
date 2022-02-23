using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class Addressee
    {
        public int Id { get; set; }
        public string Office { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }

        public virtual ICollection<DocData> DocDatas { get; set; }

    }
}
