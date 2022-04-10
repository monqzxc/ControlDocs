using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class Activities
    {
        public int Id { get; set; }
        public string Activity { get; set; }
        public string User { get; set; }
        public DateTime DateTime { get; set; }

        dbDocs db = new dbDocs();
        public void addActivities()
        {
            var activities = new Activities()
            {
                Activity = Activity,
                DateTime = DateTime,
                User = User
            };
            db.Activities.Add(activities);
            db.SaveChanges();
        }
    }
}
