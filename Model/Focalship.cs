namespace DocsControl.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Focalship
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Focalship()
        {
            Focals = new HashSet<Focal>();
        }

        public int Id { get; set; }

        public string Acronym { get; set; }

        public string FocalshipName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Focal> Focals { get; set; }

        dbDocs db = new dbDocs();

        public void addFocalship()
        {
            var n = new Focalship()
            {
                Acronym = Acronym,
                FocalshipName = FocalshipName
            };
            db.Focalships.Add(n);
            db.SaveChanges();
        }


        public void editFocalship()
        {
            var n = db.Focalships.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            n.FocalshipName = FocalshipName;
            n.Acronym = Acronym;
            db.SaveChanges();
        }
    }
}
