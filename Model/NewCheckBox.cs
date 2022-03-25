using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DocsControl.Model
{
    public class NewCheckBox : CheckBox
    {       
        public NewCheckBox()
        {
            Margin = new Thickness(5, 2, 0, 0);
            Width = 200;
        }

        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            Console.WriteLine(this.Tag.ToString());
        }
    }
}
