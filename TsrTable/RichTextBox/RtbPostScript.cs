using C1.WPF.RichTextBox.Documents;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TsrTable.RichTextBox.TableData;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{
    public sealed class RtbPostScript : C1Paragraph, IRtbElement, IRtbPostScript
    {
        public RtbPostScript()
        {
            Foreground = System.Windows.Media.Brushes.Red;
        }

        //public RtbPostScript(Brush brush, RoutedEventHandler action) : this(brush)
        //{
        //    SetAction(action);
        //}
        public RtbPostScript(Brush brush)
        {
            Foreground = brush;
            IsEditable = false;
        }

        public void SetAction(RoutedEventHandler action)
        {
            var button = new Button()
            {
                Content = "編集",
                FontSize = 8,
            };
            button.Click += action;
            button.Tag = this;
            Children.OfType<C1Block>().Last().
                Children.Add(new RtbButtonContainer(button));
        }

        public ITsrElement GetTsrInstance()
            => new TsrPostScript(Foreground);

    }
}
