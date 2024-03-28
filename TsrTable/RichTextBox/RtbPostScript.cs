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
        private C1Run _run;
        private C1InlineUIContainer _container;

        public string Text
        {
            get
            {
                return _run.Text;
            }
            private set
            {
                _run.Text = value;
            }
        }
        private Color _color;
        public Color Color
        {
            get { return _color; }
            private set
            {
                _color = value;
                _run.Foreground = new SolidColorBrush(_color);
            }
        }

        public RtbPostScript() { }
        public RtbPostScript(string text, Color color)
        {
            _run = new C1Run()
            {
                Background = null,
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
            };
            Children.Add(_run);
            Text = text;
            Color = color;
        }

        public RtbPostScript(string text, Color color, RoutedEventHandler action) :
             this(text, color)
        {
            var button = new Button()
            {
                Content = "編集",
                FontSize = 8,
            };
            button.Click += action;
            button.Tag = this;

            _container = new C1InlineUIContainer() { };
            _container.Content = button;

            Children.Add(_container);
        }

        public void EditText(string text, Color color)
        {
            if (string.IsNullOrEmpty(text))
            {
                this.Parent.Children.Remove(this);
                return;
            }
            Text = text;
            Color = color;
        }

        public ITsrElement ToTsr()
        {
            return new TsrPostScript(Text, Color);
        }
    }
}
