namespace DocsControl.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Focal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Focal()
        {
            DocDatas = new HashSet<DocData>();
            DocDatas1 = new HashSet<DocData>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public double? ContactNumber { get; set; }

        public string Email { get; set; }
        public int PlantillaID { get; set; }

        public int FocalshipID { get; set; }

        public int OfficeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocData> DocDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocData> DocDatas1 { get; set; }

        public virtual Focalship Focalship { get; set; }

        public virtual Office Office { get; set; }

        public virtual Plantilla Plantilla { get; set; }
    }
}