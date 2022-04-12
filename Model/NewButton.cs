using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DocsControl.Model
{
    public class NewButton : Button
    {
        
        public NewButton()
        {
            Width = 40;
            Height = 25;
            Margin = new Thickness(3, 0, 0, 0);
            Foreground = Brushes.White;
            Cursor = Cursors.Hand;
            Background = Brushes.Teal;            
        }
        protected override void OnClick()
        {
            Background = Brushes.OrangeRed;
            MessageBox.Show(this.Content.ToString());
            base.OnClick();
        }
    }
}
