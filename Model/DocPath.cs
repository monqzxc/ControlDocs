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
        public string DocStatusTag { get; set; }
        public virtual DocData DocData { get; set; }

        dbDocs db = new dbDocs();
        public List<string> pathList = new List<string>();
        public List<string> pathItem = new List<string>();
        public void addPath()
        {
            if (pathList.Count > 0)
            {
                foreach (var item in pathList)
                {
                    var doc = new DocPath()
                    {                        
                        DocDataID = DocDataID, 
                        DocStatusTag = item.Split('|')[1],
                        Path = item.Split('|')[0]
                    };
                    db.DocPaths.Add(doc);
                    db.SaveChanges();
                }
            }            
        }

        public void deletePath()
        {
            if (pathItem.Count > 0)
            {
                foreach (var item in pathItem)
                {
                    var doc = db.DocPaths.Where(x => x.Path.Equals(item)).FirstOrDefault();
                    db.DocPaths.Remove(doc);
                    db.SaveChanges();
                    deletePath(item);
                }
            }  
        }
        private void deletePath(string path)
        {
            try
            {
                System.IO.File.Delete(path);
            }
            catch (Exception)
            {
                //sorry medyo hirap ako dito kaya nandaya ako using try catch
            }
               
        }
        public IQueryable<DocPath> GetDocPaths(string Tag)
        {
            return db.DocPaths.Where(x => x.DocDataID.Equals(DocDataID) && x.DocStatusTag.Equals(Tag));
        }       
    }
}
