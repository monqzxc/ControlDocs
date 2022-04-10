using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class Plantilla : INotifyPropertyChanged
    {

        public int Id { get; set; }
        private string _item;
        public string Item
        {
            get
            {
                return _item;
            }
            set { _item = value;
                NotifyPropertyChanged();
            }
        }

        private string _acronym;
        public string Acronym
        {
            get
            {
                return _acronym;
            }
            set
            {
                _acronym = value;
                NotifyPropertyChanged();
            }
        }

        public virtual ICollection<Focal> Focals { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        dbDocs db = new dbDocs();
        public void addPlanntilla()
        {
            var n = new Plantilla()
            {
                Acronym = Acronym,
                Item = Item
            };
            db.Plantillas.Add(n);
            db.SaveChanges();
        }

        public void editPlantilla()
        {
            var n = db.Plantillas.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            n.Item = Item;
            n.Acronym = Acronym;
            db.SaveChanges();
        }
    }
}
