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
    /// PostScriptWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PostScriptWindow : Window
    {
        public string Text { get; set; } = string.Empty;
        public PostScriptWindow()
        {
            InitializeComponent();
        }

        public PostScriptWindow(string text)
        {
            InitializeComponent();
            PostScriptText.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Text=PostScriptText.Text;
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
            this.Close();
        }
    }
}
