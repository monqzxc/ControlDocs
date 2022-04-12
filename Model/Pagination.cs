using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsControl.Model
{
    public class Pagination
    {
        private int MaxPage = 12;
        public int TotalDocs { get; set; }
        private int firstIndex = 1;
        public int skip = 0;

        public List<NewButton> NewButtons
        {
            get
            {
                var buttonList = new List<NewButton>();
                var totalPage = TotalDocs <= MaxPage ? 1 : TotalDocs / MaxPage;
                for (int i = 0; i < totalPage; i++)
                {
                    if (i > 5)
                    {
                        buttonList.Add(new NewButton()
                        {
                            Content = ">"                            
                        });
                        break;
                    }
                    buttonList.Add(new NewButton()
                    {
                        Content = i + firstIndex
                    });                    
                }
                return buttonList;
            }            
        }


    }
}
