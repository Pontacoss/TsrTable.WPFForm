using C1.WPF.RichTextBox.Documents;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TsrTable.RichTextBox
{
    public sealed class TsrPostScript : C1Span
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

        public TsrPostScript() { }
        public TsrPostScript(string text, Color color)
        {
            _run = new C1Run()
            {
                Background = null,
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
            };

            // 編集ボタン格納用のUIContainerを作成して入れておく
            _container = new C1InlineUIContainer() { };
            Children.Add(_run);
            Children.Add(_container);

            Text = text;
            Color = color;
        }

        public void AddButton(Button button)
        {
            button.Tag = this;
            _container.Content = button;
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
    }
}
