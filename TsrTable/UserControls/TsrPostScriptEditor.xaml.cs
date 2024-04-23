using C1.WPF.RichTextBox.Documents;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TsrTable.RichTextBox;

namespace TsrTable.UserControls
{
    /// <summary>
    /// TsrPostScriptEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TsrPostScriptEditor : UserControl
    {
        public C1TextElement NewValue { get; private set; }

        public Brush Brush
        {
            get
            {
                return new SolidColorBrush(MyColorPicker.SelectedColor);
            }
            private set
            {
                MyColorPicker.SelectedColor = (value as SolidColorBrush).Color;
            }
        }

        private RoutedEventHandler _action;

        public TsrPostScriptEditor(
            RoutedEventHandler action)
        {
            _action = action;
            InitializeComponent();
            PostScriptRichTextBox.DefaultParagraphMargin = new Thickness(0);

            this.Name = "PostScriptEditor";
        }

        public TsrPostScriptEditor(
            C1TextElement rtbPostScript,
            RoutedEventHandler action) : this(action)
        {
            var IsInline = rtbPostScript is RtbInlinePostScript;
            Brush = rtbPostScript.Foreground;

            rtbPostScript.EnumerateSubtree()
                .OfType<RtbButtonContainer>().FirstOrDefault()?.Remove();

            C1TextElement baseParagraph;
            if (IsInline)
            {
                baseParagraph = PostScriptRichTextBox.Document.
                    Children.First(x => x.GetType() == typeof(C1Paragraph));
            }
            else
            {
                baseParagraph = PostScriptRichTextBox.Document;
            }

            baseParagraph.Children.Clear();

            foreach (var item in rtbPostScript.Children)
            {
                if (!(item is RtbButtonContainer))
                {
                    baseParagraph.Children.Add(item.Clone());
                }
            }
            ChangeColor(Brush);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PostScriptRichTextBox.Text))
            {
                Window.GetWindow(this).DialogResult = false;
            }
            else
            {
                Window.GetWindow(this).DialogResult = true;

                var paragraphCount =
                    PostScriptRichTextBox.Document.
                    EnumerateSubtree().OfType<C1Block>().Count();

                C1TextElement baseParagraph;
                if (paragraphCount == 1)
                {
                    baseParagraph =
                        PostScriptRichTextBox.Document.
                        Children.First(x => x is C1Block);
                    NewValue = new RtbInlinePostScript(Brush, _action);
                }
                else
                {
                    baseParagraph = PostScriptRichTextBox.Document;
                    NewValue = new RtbPostScript(Brush);
                }

                int counter = 0;
                foreach (var element in baseParagraph.Children)
                {
                    NewValue.Children.Insert(counter, element.Clone());
                    counter++;
                }
                if (NewValue is RtbPostScript outline)
                {
                    outline.SetAction(_action);
                }
            }
            Window.GetWindow(this).Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            Window.GetWindow(this).Close();
        }

        private void ChangeColor(Brush brush)
        {
            foreach (var element in PostScriptRichTextBox.Document.EnumerateSubtree())
            {
                element.Foreground = brush;
            }
        }

        private void C1ColorPicker_SelectedColorChanged(object sender, C1.WPF.PropertyChangedEventArgs<Color> e)
        {
            var brush = new SolidColorBrush(e.NewValue);
            ChangeColor(brush);
        }

        private void InsertParameterButton_Click(object sender, RoutedEventArgs e)
        {
            PostScriptRichTextBox.InsertParameter("パラメータ");
        }

        private void InsertSubScriptButton_Click(object sender, RoutedEventArgs e)
        {
            PostScriptRichTextBox.InsertSuperSubScript();
        }

        private void InsertBulletPointButton_Click(object sender, RoutedEventArgs e)
        {
            PostScriptRichTextBox.InsertBullet();
        }

        private void SubTitleButton_Click(object sender, RoutedEventArgs e)
        {
            PostScriptRichTextBox.InsertSubTitle();
        }
    }
}
