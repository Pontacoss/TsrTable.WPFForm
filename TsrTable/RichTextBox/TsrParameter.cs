using C1.WPF.RichTextBox.Documents;
using System.Windows;
using System.Windows.Media;

namespace TsrTable.RichTextBox
{
    public sealed class TsrParameter : C1Span
    {
        static int count;
        public TsrParameter() { }
        public TsrParameter(string name)
        {
            count++;
            var run = new C1Run()
            {
                Text = "[[" + name + ":" + count + "]]",
                IsEditable = false,
                Background = new SolidColorBrush(Colors.Pink),
                Padding = new Thickness(0, 0, 0, 0),
            };
            Children.Add(run);
            IsEditable = false;
        }
    }
}
