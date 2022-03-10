namespace DocsControl.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class DocData //: DocPath
        {
        public int Id { get; set; }

        public string DocSubject { get; set; }
        public string DocControlNumber { get; set; }
        public string Remarks { get; set; }
        public string CurrentStatus { get; set; }

        public DateTime? ForSigned { get; set; }

        public DateTime? Signed { get; set; }

        public DateTime? ForRelease { get; set; }
       
        public string DoctTypes { get; set; }
        
        public DateTime DateAdd { get; set; }
        public string Tag { get; set; }

        public int FocalID { get; set; }
        public int AddresseeID { get; set; }
        public virtual Focal Focal { get; set; }

        public virtual Addressee Addressee { get; set; }

        public virtual ICollection<DocPath> DocPaths { get; set; }

        public IQueryable<DocData> GetUserInfo()
        {
            return new dbDocs().DocDatas;
        }

        dbDocs db = new dbDocs();
        public void addDocData()
        {                        
            var doc = new DocData()
            {
                DocSubject = DocSubject,
                CurrentStatus = CurrentStatus,
                ForSigned = ForSigned,
                Signed = Signed,
                ForRelease = ForRelease,
                FocalID = FocalID,
                DateAdd = DateAdd,
                DoctTypes = DoctTypes,
                AddresseeID = AddresseeID,
                Remarks = Remarks,
                Tag = Tag,
                DocControlNumber = DocControlNumber
            };
           
            db.DocDatas.Add(doc);
            db.SaveChanges();
        }

        public void editDocData()
        {
            var doc = db.DocDatas.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            doc.DocSubject = DocSubject;
            doc.CurrentStatus = CurrentStatus;
            doc.ForSigned = ForSigned;
            doc.Signed = Signed;
            doc.ForRelease = ForRelease;
            doc.FocalID = FocalID;
            doc.DateAdd = DateAdd;
            doc.DoctTypes = DoctTypes;
            doc.AddresseeID = AddresseeID;
            doc.Remarks = Remarks;
            doc.Tag = Tag;
            doc.DocControlNumber = DocControlNumber;
            db.SaveChanges();
        }

        public void deleteDocData()
        {

        }
    }
}
