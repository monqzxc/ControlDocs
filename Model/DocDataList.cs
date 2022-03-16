using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class DocDataList : INotifyPropertyChanged
    {
      
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<DocData> _docDataList;


        dbDocs db = new dbDocs();

        public ObservableCollection<DocData> newList
        {
            get
            {
                var docs = db.DocDatas.Where(x => x.CurrentStatus.Equals("FOR SIGNATURE")).ToList();
                foreach (var item in docs)
                {
                     _docDataList.Add(new DocData()
                    {
                        Id = item.Id,
                        DocSubject = item.DocSubject,
                        ForSigned = item.ForSigned
                    });
                }
                return _docDataList;
            }
            set
            {
                _docDataList = value;
                NotifyPropertyChanged();
            }
        }



    }
}
