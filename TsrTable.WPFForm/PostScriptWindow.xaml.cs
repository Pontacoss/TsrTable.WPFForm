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
        public RtbPostScript NewValue { get; private set; }

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

        public PostScriptWindow(RtbPostScript rtbPostScript, RoutedEventHandler action) : this(action)
        {
            Brush = rtbPostScript.Foreground;
            var button = rtbPostScript.EnumerateSubtree().OfType<RtbButtonContainer>().FirstOrDefault();
            if (button != null)
            {
                button.Remove();
            }

            PostScriptRichTextBox.Document.Remove(0, PostScriptRichTextBox.Document.Count());

            foreach (var item in rtbPostScript.Children)
            {
                if (!(item is RtbButtonContainer))
                {
                    PostScriptRichTextBox.Document.Children.Add(item.Clone());
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
                //todo 追記の仕方2パターン　paragraph内かSpanか
                DialogResult = true;
                NewValue = new RtbPostScript(Brush);

                foreach (var element in PostScriptRichTextBox.Document.Children)
                {
                    NewValue.Children.Add(element.Clone());
                }
                NewValue.SetAction(_action);
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
            if (RtbSentenceTools.RemoveBullet(PostScriptRichTextBox)) return;
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
