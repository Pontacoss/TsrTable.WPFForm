using System.Windows;
using System.Windows.Controls;

namespace TsrTable.UserControls
{
    /// <summary>
    /// TsrSubTitleEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TsrSubTitleEditWindow : UserControl
    {
        public string Text
        {
            get
            {
                return SubTitleText.Text ?? string.Empty;
            }
            private set
            {
                SubTitleText.Text = value ?? string.Empty;
            }
        }
        public TsrSubTitleEditWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
            //this.Close();
        }
    }
}
