namespace DocsControl.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DocData
    {
        public int Id { get; set; }

        public string DocSubject { get; set; }

        public string CurrentStatus { get; set; }

        public DateTime? ForSigned { get; set; }

        public DateTime? Signed { get; set; }

        public DateTime? ForRelease { get; set; }

        public string Link { get; set; }

        public string DoctTypes { get; set; }


        public DateTime DateAdd { get; set; }

        public virtual Focal Focal { get; set; }

        public virtual Addressee Addressee { get; set; }
    }
}
