using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class Plantilla
    {

        public int Id { get; set; }
        public string Item { get; set; }
        public string Acronym { get; set; }

        public virtual ICollection<Focal> Focals { get; set; }
    }
}
