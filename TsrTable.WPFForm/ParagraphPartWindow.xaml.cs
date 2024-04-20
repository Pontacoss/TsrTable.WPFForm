using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            rtb.FontSize = 10.5;
            rtb.ViewMode = TextViewMode.Draft;
            rtb.Zoom = 1.3;
            rtb.HideSelection = true;
            rtb.DefaultParagraphMargin = new Thickness(0, 0, 0, 0);

            var para4 = new C1Paragraph();
            var para2 = new C1Paragraph();
            var run2 = new C1Run();
            var para3 = new C1Paragraph();
            var run3 = new C1Run();
            var run4 = new C1Run();

            var run1 = rtb.Document.Children[0].Children[0] as C1Run;
            run1.Text = "＜試験仕様＞ \r\nHCN-P827： 6.1項を参照すること。\r\n\r\n";

            run2.Text = " ＜所内向け追加指示および注意事項＞ \r\n" +
                "銘板、表記は外形図：H7R2149と一致していることを確認する。";
            para2.Children.Add(run2);
            run3.Text = "その他詳細は、伊ミキ - 63811に従うこと。なお伊ミキ - 63811は以下の項目を記載している。" +
                " \r\n3.1\t一般外観検査\t\t\t3.9\tカバー状態検査" +
                " \r\n3.2\t絶縁物検査\t\t\t3.10\t表面処理検査 \r\n" +
                "3.3\t取付け状態検査\t\t3.11\t表示検査 \r\n3.4\t締付け状態検査\t\t" +
                "3.12\t銘板検査 \r\n3.5\t配線及び結線検査\t\t3.13\t付属品検査 \r\n" +
                "3.6\t動作調整検査\t\t\t3.14\t配管検査 \r\n3.7\t電導接触部検査\t\t" +
                "3.15\t光ファイバーケーブル配線検査 \r\n3.8\t寸法検査\r\n\r\n";
            para3.Children.Add(run3);

            run4.Text = "IECの項目分けに従い、配線チェックをVisual Inspectionの項目に統合している。配線チェックを忘れず行うこと。" +
                " \r\n組立図：　H14E673～H14E677\t \r\nWIRING DIAGRAM (主回路): H14E516 \r\nWIRING DIAGRAM (制御回路): H14E695 ";
            para4.Children.Add(run4);

            rtb.Document.Children.Add(para2);
            rtb.Document.Children.Add(para3);
            rtb.Document.Children.Add(para4);

            // 設定用のダイアログを表示するため、PanelがあるWindowを登録
            TsrFacade.EditWindow = new BlankWindow();
        }

        private void InsertParameterButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.InsertParameter("パラメータ");
        }

        private void InsertSuperSubScriptButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.InsertSuperSubScript();
        }

        private void InsertStrikethroughButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.InsertStrikethrough();
        }

        private void InsertBulletPointButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.InsertBullet();
        }

        private void InsertPostScriptButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.InsertPostScript();
        }

        private void InsertSubTitleButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.InsertSubTitle();
        }

        private void EditableChangeButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.IsReadOnly = rtb.IsReadOnly != true;
        }

        private void SerializeButton_Click(object sender, RoutedEventArgs e)
        {
            var jsonText = TsrFacade.Serialize(rtb, IndentedCheckBox.IsChecked);
            JsonTextBox.Text = jsonText;
            JsonTextCountTextBlock.Text = jsonText.Count().ToString();
        }
        private void DeserializeButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.Deserialize(JsonTextBox.Text);
        }

        private void WindowClosed(object sender, System.EventArgs e)
        {
            TsrFacade.EditWindow = null;
        }

        /// <summary>
        /// 文章中にある取消線を全てリスト化する。
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private List<C1Run> SeekStrikethroughRecrusion(C1TextElement element)
        {
            var list = new List<C1Run>();
            foreach (var child in element.Children)
            {
                if (child is C1Run run)
                {
                    if (run.TextDecorations
                        == C1TextDecorations.Strikethrough)
                    {
                        list.Add(run);
                    }
                }
                else
                {
                    list.AddRange(SeekStrikethroughRecrusion(child));
                }
            }
            return list;
        }
    }
}
