using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace DocsControl.Model
{
    public class Modules
    {
        public static void ComboBox(System.Windows.Controls.ComboBox cmb, IEnumerable<string> data)
        {
            cmb.Items.Clear();
            var list = new List<string>();
           
            foreach (var item in data.ToList())
            {
                list.Add(item);
            }            
            cmb.ItemsSource = list;
            cmb.SelectedIndex = 0;
        }

        public static void showInfo(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void showError(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //opening dialog. 
        //currently disabled
        //public static void openDialog(Window window, Window window2)
        //{
        //    window.Opacity = .5;
        //    window.Background = Brushes.Black;
        //    window2.ShowDialog();
        //    window.Opacity = 1;
        //    window.Background = Brushes.Transparent;
        //}
    }
}
