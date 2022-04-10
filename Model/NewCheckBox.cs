using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static DocsControl.Model.Modules;

namespace DocsControl.Model
{
    public class NewCheckBox : CheckBox
    {       
        public NewCheckBox()
        {
            Margin = new Thickness(5, 6, 0, 0);
            Width = 123;
            //continue the loading of focals checked na dapat sila hehe
        }
            
        protected override void OnChecked(RoutedEventArgs e)
        {
            checkList.Add(this.Tag.ToString());            
        }
        protected override void OnUnchecked(RoutedEventArgs e)
        {
            checkList.Remove(this.Tag.ToString());
        }
    }
}
