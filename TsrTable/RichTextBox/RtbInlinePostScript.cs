using C1.WPF.RichTextBox.Documents;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TsrTable.RichTextBox.TableData;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{
    public sealed class RtbInlinePostScript : C1Span, IRtbElement, IRtbPostScript
    {
        public RtbInlinePostScript()
        {
            Foreground = System.Windows.Media.Brushes.Red;
        }

        public RtbInlinePostScript(Brush brush, RoutedEventHandler action) : this(brush)
        {
            SetAction(action);
        }
        public RtbInlinePostScript(Brush brush)
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

            Children.Add(new RtbButtonContainer(button));
        }

        public ITsrElement GetTsrInstance()
            => new TsrInlinePostScript(Foreground);

    }
}
