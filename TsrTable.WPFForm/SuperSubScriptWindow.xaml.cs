using System.Windows;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// SuperSubScriptWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SuperSubScriptWindow : Window
    {
        public string SuperScriptString { get; set; } = string.Empty;
        public string SubScriptString { get; set; } = string.Empty;
        public string BaseScriptString { get; set; } = string.Empty;

        public SuperSubScriptWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BaseScriptString = BaseScriptTextBox.Text;
            SuperScriptString = SuperScriptTextBox.Text;
            SubScriptString = SubScriptTextBox.Text;
            this.Tag = (BaseScriptString, SuperScriptString, SubScriptString);
            this.Close();
        }
    }
}
