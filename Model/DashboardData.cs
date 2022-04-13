using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class DashboardData
    {
        public string Status { get; set; }
        public int Today { get; set; }
        public int Yesterday { get; set; }
        public int Week { get; set; }
        public int Month { get; set; }
        public int Average { get; set; }

    }
}
