using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// SuperSubScriptWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SuperSubScriptWindow : Window
    {
        public string SuperScriptString { get; set; }=string.Empty;
        public string SubScriptString { get; set; } = string.Empty;
        public string BaseScriptString { get; set; } = string.Empty;

        public SuperSubScriptWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BaseScriptString=BaseScriptTextBox.Text;
            SuperScriptString=SuperScriptTextBox.Text;
            SubScriptString=SubScriptTextBox.Text;
            this.Close();
        }
    }
}
