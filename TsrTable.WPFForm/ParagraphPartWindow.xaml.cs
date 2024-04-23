using System.Linq;
using System.Windows;
using TsrTable.Fake;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// ParagraphPartWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ParagraphPartWindow : Window
    {
        public ParagraphPartWindow()
        {
            InitializeComponent();

            rtb.GetInitialFake();

            // 設定用のダイアログを表示するため、PanelがあるWindowを登録
            //rtb.EditWindow = new BlankWindow();
        }

        private void InsertParameterButton_Click(
            object sender, RoutedEventArgs e) => rtb.InsertParameter("パラメータ");

        private void InsertSuperSubScriptButton_Click(
            object sender, RoutedEventArgs e) => rtb.InsertSuperSubScript();

        private void InsertStrikethroughButton_Click(
            object sender, RoutedEventArgs e) => rtb.InsertStrikethrough();

        private void InsertBulletPointButton_Click(
            object sender, RoutedEventArgs e) => rtb.InsertBullet();

        private void InsertPostScriptButton_Click(
            object sender, RoutedEventArgs e) => rtb.InsertPostScript();

        private void InsertSubTitleButton_Click(
            object sender, RoutedEventArgs e) => rtb.InsertSubTitle();

        private void EditableChangeButton_Click(
            object sender, RoutedEventArgs e) =>
            rtb.IsReadOnly = rtb.IsReadOnly != true;

        private void SerializeButton_Click(object sender, RoutedEventArgs e)
        {
            var jsonText = rtb.Serialize(IndentedCheckBox.IsChecked);
            JsonTextBox.Text = jsonText;
            JsonTextCountTextBlock.Text = jsonText.Count().ToString();
        }

        private void DeserializeButton_Click(
            object sender, RoutedEventArgs e) => rtb.Deserialize(JsonTextBox.Text);

        //private void WindowClosed(
        //    object sender, System.EventArgs e) => rtb.EditWindow = null;

        private void ViewModeChangeButton_Click(
            object sender, RoutedEventArgs e) => rtb.ViewModeChange();

        private void ZoomOutButton_Click(
            object sender, RoutedEventArgs e) => rtb.ZoomOut();

        private void ZoomInButton_Click(
            object sender, RoutedEventArgs e) => rtb.ZoomIn();


        /// <summary>
        /// 文章中にある取消線を全てリスト化する。
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        //private List<C1Run> SeekStrikethroughRecrusion(C1TextElement element)
        //{
        //    var list = new List<C1Run>();
        //    foreach (var child in element.Children)
        //    {
        //        if (child is C1Run run)
        //        {
        //            if (run.TextDecorations
        //                == C1TextDecorations.Strikethrough)
        //            {
        //                list.Add(run);
        //            }
        //        }
        //        else
        //        {
        //            list.AddRange(SeekStrikethroughRecrusion(child));
        //        }
        //    }
        //    return list;
        //}
    }
}
