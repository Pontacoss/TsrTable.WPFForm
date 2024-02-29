using C1.WPF.RichTextBox.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using System.Windows;

namespace TsrTable.RichTextBox
{
    public sealed class TsrParameter : C1Run
    {
        static int count;
        public TsrParameter() { }
        public TsrParameter(string name)
        {
            count++;
            Text = "[[" + name + ":" + count + "]]";
            IsEditable = false;
            Background = new SolidColorBrush(Colors.Pink);
            Padding = new Thickness(0, 0, 0, 0);
        }
    }
}
