using System.Windows;
using System.Windows.Controls;
using MarkerStyle = C1.WPF.RichTextBox.Documents.TextMarkerStyle;

namespace TsrTable.UserControls
{
    /// <summary>
    /// TsrBulletMarkerSelector.xaml の相互作用ロジック
    /// </summary>
    public partial class TsrBulletMarkerSelector : UserControl
    {
        public MarkerStyle MarkerStyle { get; private set; } = MarkerStyle.Disc;

        public TsrBulletMarkerSelector()
        {
            InitializeComponent();
            this.Name = "MarkerSelector";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;
            if (button.Name == "CircleButton")
                MarkerStyle = MarkerStyle.Circle;
            else if (button.Name == "SquareButton")
                MarkerStyle = MarkerStyle.Square;
            else if (button.Name == "DiscButton")
                MarkerStyle = MarkerStyle.Disc;
            else if (button.Name == "DecimalButton")
                MarkerStyle = MarkerStyle.Decimal;
            else if (button.Name == "BoxButton")
                MarkerStyle = MarkerStyle.Box;
            else if (button.Name == "LowerRomanButton")
                MarkerStyle = MarkerStyle.LowerRoman;
            else if (button.Name == "UpperLatinButton")
                MarkerStyle = MarkerStyle.UpperLatin;
            else if (button.Name == "LowerLatinButton")
                MarkerStyle = MarkerStyle.LowerLatin;

            Window.GetWindow(this).Close();
        }
    }
}
