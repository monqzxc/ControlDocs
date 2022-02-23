using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DocsControl.Model
{
    public class Modules
    {
        public void ComboBox(ComboBox cmb, IEnumerable<string> data, string title)
        {
            //cmb.Items.Clear();
            var list = new List<string>();
            list.Add($"-- {title} --");


            foreach (var item in data.ToList())
            {
                list.Add(item);
            }
            cmb.ItemsSource = list;
            cmb.SelectedIndex = 0;
        }
    }
}
