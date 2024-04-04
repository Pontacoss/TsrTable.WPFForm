using C1.WPF.RichTextBox.Documents;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TsrTable.RichTextBox.TableData;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{
    public sealed class RtbPostScript : C1Span, IRtbElement
    {
        public Color Color { get; private set; }

        public RtbPostScript() { }
        public RtbPostScript(Color color)
        {
            Color = color;
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

        public void EditText(Color color)
        {
            //if (string.IsNullOrEmpty(RtbContent.Text))
            //{
            //    this.Parent.Children.Remove(this);
            //    return;
            //}
            Color = color;
        }

        public ITsrElement GetTsrInstance()
            => new TsrPostScript(Color);

    }
}
