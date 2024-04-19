using C1.WPF.RichTextBox.Documents;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TsrTable.RichTextBox;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// PostScriptWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PostScriptWindow : Window
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

        public PostScriptWindow(RoutedEventHandler action)
        {
            _action = action;
            InitializeComponent();
            PostScriptRichTextBox.DefaultParagraphMargin = new Thickness(0);
        }

        public PostScriptWindow(C1TextElement rtbPostScript, RoutedEventHandler action)
        {
            _action = action;
            InitializeComponent();
            var IsInline = rtbPostScript is RtbInlinePostScript;

            PostScriptRichTextBox.DefaultParagraphMargin = new Thickness(0);

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
                DialogResult = false;
            }
            else
            {
                DialogResult = true;

                var paragraphCount = PostScriptRichTextBox.Document.EnumerateSubtree().OfType<C1Block>().Count();

                C1TextElement baseParagraph;
                if (paragraphCount == 1)
                {
                    baseParagraph = PostScriptRichTextBox.Document.Children.First(x => x is C1Block);
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
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
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
            var fm = new SuperSubScriptWindow();
            fm.ShowDialog();

            if (fm.BaseScriptString == string.Empty) return;
            if (fm.SuperScriptString != string.Empty)
            {
                PostScriptRichTextBox.InsertSuperScript(fm.BaseScriptString, fm.SuperScriptString);
            }
            else if (fm.SubScriptString != string.Empty)
            {
                PostScriptRichTextBox.InsertSubScript(fm.BaseScriptString, fm.SubScriptString);
            }
            else return;
        }

        private void InsertBulletPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (RtbFacade.RemoveBullet(PostScriptRichTextBox)) return;
            var fm = new BulletControlWindow();
            fm.ShowDialog();
            PostScriptRichTextBox.InsertBullet(fm.MarkerStyle);
        }

        private void SubTitleButton_Click(object sender, RoutedEventArgs e)
        {
            var fm = new SubTitleWindow();
            fm.ShowDialog();
            PostScriptRichTextBox.InsertSubTitle(fm.Text);
        }
    }
}
