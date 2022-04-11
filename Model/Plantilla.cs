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
        public string Position { get; set; }
        public string Acronym { get; set; }

        public virtual ICollection<Focal> Focals { get; set; }

        dbDocs db = new dbDocs();
        public void addPlanntilla()
        {
            var n = new Plantilla()
            {
                Acronym = Acronym,
                Position = Position
            };
            db.Plantillas.Add(n);
            db.SaveChanges();
        }

        public void editPlantilla()
        {
            var n = db.Plantillas.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            n.Position = Position;
            n.Acronym = Acronym;
            db.SaveChanges();
        }
    }
}
