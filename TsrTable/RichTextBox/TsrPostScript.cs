using C1.WPF.RichTextBox.Documents;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using static System.Net.Mime.MediaTypeNames;

namespace TsrTable.RichTextBox
{
    public sealed class TsrPostScript : C1Span
    {
        public string Text 
        {
            get
            {
                var run = Children.First(x => x.GetType() == typeof(C1Run)) as C1Run;
                return run.Text;
            }
            private set 
            {
                var run = Children.First(x => x.GetType() == typeof(C1Run)) as C1Run;
                run.Text = value;
            }

        }
        public TsrPostScript() { }
        public TsrPostScript(string text)
        {
            var run = new C1Run()
            {
                Foreground = System.Windows.Media.Brushes.Blue,
                Background = null,
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
            };

            var container = new C1InlineUIContainer() { };
            Children.Add(run);
            Children.Add(container);

            Text = text;
        }

        public void AddButton(Button button)
        {
            button.Tag = this;
            var container=Children.First(x=>x.GetType()==typeof(C1InlineUIContainer)) as C1InlineUIContainer;
            container.Content = button;
        }

        public void EditText(string text)
        {
            Text=text;
        }
    }
}
