using System.Windows;
using System.Windows.Media;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// PostScriptWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PostScriptWindow : Window
    {
        public string Text
        {
            get
            {
                return PostScriptText.Text ?? string.Empty;
            }
            private set
            {
                PostScriptText.Text = value ?? string.Empty;
            }
        }
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

        public PostScriptWindow(string text, Color color)
        {
            InitializeComponent();
            Text = text;
            Color = color;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
            this.Close();
        }

        private void C1ColorPicker_SelectedColorChanged(object sender, C1.WPF.PropertyChangedEventArgs<Color> e)
        {
            PostScriptText.Foreground = new SolidColorBrush(e.NewValue);
        }
    }
}
