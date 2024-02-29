using System.Windows;
using TextMarkerStyle = C1.WPF.RichTextBox.Documents.TextMarkerStyle;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// BulletControlWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class BulletControlWindow : Window
    {
        public TextMarkerStyle MarkerStyle;
        public BulletControlWindow()
        {
            InitializeComponent();
        }

        private void CircleButton_Click(object sender, RoutedEventArgs e)
        {
            MarkerStyle = TextMarkerStyle.Circle;
            this.Close();
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            MarkerStyle = TextMarkerStyle.Square;
            this.Close();
        }

        private void LowerLatinButton_Click(object sender, RoutedEventArgs e)
        {
            MarkerStyle = TextMarkerStyle.LowerLatin;
            this.Close();
        }

        private void UpperLatinButton_Click(object sender, RoutedEventArgs e)
        {
            MarkerStyle = TextMarkerStyle.UpperLatin;
            this.Close();
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            MarkerStyle = TextMarkerStyle.Decimal;
            this.Close();
        }

        private void LowerRomanButton_Click(object sender, RoutedEventArgs e)
        {
            MarkerStyle = TextMarkerStyle.LowerRoman;
            this.Close();
        }

        private void DiscButton_Click(object sender, RoutedEventArgs e)
        {
            MarkerStyle = TextMarkerStyle.Disc;
            this.Close();
        }

        private void BoxButton_Click(object sender, RoutedEventArgs e)
        {
            MarkerStyle = TextMarkerStyle.Box;
            this.Close();
        }
    }
}
