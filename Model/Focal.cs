namespace DocsControl.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Focal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Focal()
        {
           
        }

        public int Id { get; set; }

        public string FullName { get; set; }
        public string NickName { get; set; }
        public double? ContactNumber { get; set; }

        public string Email { get; set; }
        public int PlantillaID { get; set; }

        public int FocalshipID { get; set; }

        public int OfficeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual Focalship Focalship { get; set; }

        public virtual Office Office { get; set; }

        public virtual Plantilla Plantilla { get; set; }

        dbDocs db = new dbDocs();


       
    }
}
