using System.Windows;
using System.Windows.Controls;
using TextMarkerStyle = C1.WPF.RichTextBox.Documents.TextMarkerStyle;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// BulletControlWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class BulletControlWindow : Window
    {
        public TextMarkerStyle MarkerStyle { get; private set; } = TextMarkerStyle.Disc;

        public BulletControlWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;
            if (button.Name == "CircleButton")
                MarkerStyle = TextMarkerStyle.Circle;
            else if (button.Name == "SquareButton")
                MarkerStyle = TextMarkerStyle.Square;
            else if (button.Name == "DiscButton")
                MarkerStyle = TextMarkerStyle.Disc;
            else if (button.Name == "DecimalButton")
                MarkerStyle = TextMarkerStyle.Decimal;
            else if (button.Name == "BoxButton")
                MarkerStyle = TextMarkerStyle.Box;
            else if (button.Name == "LowerRomanButton")
                MarkerStyle = TextMarkerStyle.LowerRoman;
            else if (button.Name == "UpperLatinButton")
                MarkerStyle = TextMarkerStyle.UpperLatin;
            else if (button.Name == "LowerLatinButton")
                MarkerStyle = TextMarkerStyle.LowerLatin;
            this.Close();
        }

    }
}
