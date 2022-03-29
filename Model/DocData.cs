namespace DocsControl.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public partial class DocData : INotifyPropertyChanged
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
        public string FocalID { get; set; }
        public int AddresseeID { get; set; }      

        public virtual Addressee Addressee { get; set; }

        public virtual ICollection<DocPath> DocPaths { get; set; }                                     

        dbDocs db = new dbDocs();
        
        public event PropertyChangedEventHandler PropertyChanged;
     
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
            doc.Signed = Signed;
            doc.ForRelease = ForRelease;
            doc.FocalID = FocalID;          
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
        public IQueryable<DocData> GetDocDatas()
        {
            return db.DocDatas.Where(x => x.Id.Equals(Id));
        }
        public List<string> GetFocals() //doing this for listing of all focals based on their id
        {
            var focalID = db.DocDatas.Where(x => x.Id.Equals(Id)).Select(x => x.FocalID).FirstOrDefault().Split(',').ToList();
            var focals = new List<string>();

            foreach (var item in focalID)
            {
                var fID = int.Parse(item);
                focals.Add(db.Focals.Where(x => x.Id.Equals(fID)).Select(x => x.NickName).FirstOrDefault());
            }
            return focals;
        }
    }
}
