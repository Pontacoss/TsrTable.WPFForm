using System.Windows;
using System.Windows.Controls;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// BlankWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class BlankWindow : Window
    {
        public BlankWindow(UserControl myControl)
        {
            InitializeComponent();
            MainPanel.Children.Add(myControl);
        }
    }
}
