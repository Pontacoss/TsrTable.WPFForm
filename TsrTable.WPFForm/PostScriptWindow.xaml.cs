using C1.WPF.RichTextBox;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TsrTable.RichTextBox;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// PostScriptWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PostScriptWindow : Window
    {
        public C1RichTextBox RtbContent => PostScriptRichTextBox;

        public Color Color
        {
            get
            {
                return MyColorPicker.SelectedColor;
            }
            private set
            {
                MyColorPicker.SelectedColor = value;
            }
        }

        public PostScriptWindow()
        {
            InitializeComponent();
        }

        public PostScriptWindow(RtbPostScript rtbPostScript) : this()
        {
            rtbPostScript.Children.FirstOrDefault(x => x.GetType() == typeof(RtbButtonContainer)).Remove();
            PostScriptRichTextBox.Document.Children.Add(rtbPostScript);
            Color = rtbPostScript.Color;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PostScriptRichTextBox.Text))
            {
                DialogResult = false;
            }
            else
                DialogResult = true;
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void C1ColorPicker_SelectedColorChanged(object sender, C1.WPF.PropertyChangedEventArgs<Color> e)
        {
            foreach (var element in PostScriptRichTextBox.Document.Children)
            {
                element.Foreground = new SolidColorBrush(e.NewValue);
            }
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

        private void PostScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var fm = new PostScriptWindow();
            fm.ShowDialog();
            PostScriptRichTextBox.InsertPostScript(fm.RtbContent, fm.Color, PostScriptInnerButton_Click);
        }
        private void PostScriptInnerButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var postScript = button.Tag as RtbPostScript;
            var fm = new PostScriptWindow(postScript);
            fm.ShowDialog();
            PostScriptRichTextBox.EditPostScript(postScript, fm.Color);
        }

        private void SubTitleButton_Click(object sender, RoutedEventArgs e)
        {
            var fm = new SubTitleWindow();
            fm.ShowDialog();
            PostScriptRichTextBox.InsertSubTitle(fm.Text);
        }
    }
}
