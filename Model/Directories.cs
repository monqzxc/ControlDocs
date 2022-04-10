using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class Directories
    {
        public User User { get; set; }
        public Focal Focal { get; set; }
        public Focalship Focalship { get; set; }
        public Plantilla Plantilla { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
