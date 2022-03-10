using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class Addressee
    {
        public int Id { get; set; }
        public string Office { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }

        public virtual ICollection<DocData> DocDatas { get; set; }

        dbDocs db = new dbDocs();
        public void addAddressee()
        {
            var addressee = new Addressee()
            {
                Office = Office,
                FullName = FullName,
                Email = Email,
                ContactNo = ContactNo
            };
            db.Addressees.Add(addressee);
            db.SaveChanges();
        }
        public void editAddressee()
        {
            var addressee = db.Addressees.Where(x => x.Id.Equals(Id)).FirstOrDefault();

            addressee.Office = Office;
            addressee.FullName = FullName;
            addressee.Email = Email;
            addressee.ContactNo = ContactNo;
                       
            db.SaveChanges();
        }

    }
}
