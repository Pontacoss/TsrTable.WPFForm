using C1.WPF.RichTextBox.Documents;
using System.Windows;
using System.Windows.Media;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{
    public sealed class RtbParameter : C1Span, IRtbElement
    {
        public string ParameterName { get; }
        static int count;
        public RtbParameter() { }
        public RtbParameter(string name)
        {
            count++;
            ParameterName = "[[" + name + ":" + count + "]]";
            var run = new C1Run()
            {
                Text = ParameterName,
                IsEditable = false,
                Background = new SolidColorBrush(Colors.Pink),
                Padding = new Thickness(0, 0, 0, 0),
            };
            Children.Add(run);
            IsEditable = false;
        }

        public ITsrElement GetTsrInstance()
            => new TsrParameter(ParameterName);
    }
}
