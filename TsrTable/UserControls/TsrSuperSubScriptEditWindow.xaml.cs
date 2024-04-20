using System.Windows;
using System.Windows.Controls;

namespace TsrTable.UserControls
{
    /// <summary>
    /// TsrSubScriptEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TsrSuperSubScriptEditWindow : UserControl
    {
        public TsrSuperSubScriptEditWindow()
        {
            InitializeComponent();
            this.Name = "SuperSubScriptEditor";
        }
        public string SuperScriptString { get; set; } = string.Empty;
        public string SubScriptString { get; set; } = string.Empty;
        public string BaseScriptString { get; set; } = string.Empty;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BaseScriptString = BaseScriptTextBox.Text;
            SuperScriptString = SuperScriptTextBox.Text;
            SubScriptString = SubScriptTextBox.Text;
            Window.GetWindow(this).Close();
        }
    }
}
