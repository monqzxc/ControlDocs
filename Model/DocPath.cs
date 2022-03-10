using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class DocPath
    {
        public int Id { get; set; }
  
        public string Path { get; set; }
        public int DocDataID { get; set; }
        
        public virtual DocData DocData { get; set; }

        dbDocs db = new dbDocs();
        public List<string> pathList = new List<string>();
        public void addPath()
        {

            if (pathList.Count > 0)
            {
                foreach (var item in pathList)
                {
                    var doc = new DocPath()
                    {
                        DocDataID = DocDataID,
                        Path = item
                    };
                    db.DocPaths.Add(doc);
                    db.SaveChanges();
                }
            }            
        }

        //public void editPath()
        //{
     
        //    var doc = new DocPath()
        //    {
        //        DocDataID = DocDataID,
        //        Path = Path
        //    };
        //    db.DocPaths.Add(doc);
        //    db.SaveChanges();
        //}
    }
}
