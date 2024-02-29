using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word;
using C1.WPF.Word.Objects;
using org.jpedal.jbig2.segment;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using TsrTable.RichTextBox;

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

            rtb.Text = "＜試験仕様＞ \r\nHCN-P827： 6.1項を参照すること。" +
                " \r\n\r\n＜所内向け追加指示および注意事項＞ \r\n" +
                "銘板、表記は外形図：H7R2149と一致していることを確認する。" +
                " \r\n\r\nその他詳細は、伊ミキ-63811に従うこと。なお伊ミキ-63811は以下の項目を記載している。" +
                " \r\n3.1\t一般外観検査\t\t\t3.9\tカバー状態検査" +
                " \r\n3.2\t絶縁物検査\t\t\t3.10\t表面処理検査 \r\n" +
                "3.3\t取付け状態検査\t\t3.11\t表示検査 \r\n3.4\t締付け状態検査\t\t" +
                "3.12\t銘板検査 \r\n3.5\t配線及び結線検査\t\t3.13\t付属品検査 \r\n" +
                "3.6\t動作調整検査\t\t\t3.14\t配管検査 \r\n3.7\t電導接触部検査\t\t" +
                "3.15\t光ファイバーケーブル配線検査 \r\n3.8\t寸法検査\r\n\r\n" +
                "IECの項目分けに従い、配線チェックをVisual Inspectionの項目に統合している。配線チェックを忘れず行うこと。" +
                " \r\n組立図：　H14E673～H14E677\t \r\nWIRING DIAGRAM (主回路): H14E516 \r\nWIRING DIAGRAM (制御回路): H14E695 ";

            rtb.FontSize = 10.5;
            rtb.ViewMode = TextViewMode.Print;
            rtb.Zoom = 1.3;
        }

        private void InsertParameterButton_Click(object sender, RoutedEventArgs e)
        {

            C1TextRange selectText = rtb.Selection;
            //FontWeight? fw = this.c1RichTextBox1.Selection.FontWeight;
            //this.c1RichTextBox1.Selection.FontWeight = fw.HasValue && fw.Value == FontWeights.Bold
            //      ? FontWeights.Normal
            //      : FontWeights.Bold;

            var stat = selectText.Start;
            var statRun = stat.Element as C1Run;

            if (statRun == null) return;

            var parent = statRun.Parent;
            if (0 < stat.Offset && stat.Offset < statRun.Text.Length)
            {
                parent.Children.Insert(statRun.Index + 1, new TsrParameter("パラメータ"));
                parent.Children.Insert(statRun.Index + 2, new C1Run()
                {
                    Text = statRun.Text.Substring(stat.Offset, statRun.Text.Length - stat.Offset)
                });
                statRun.Text = statRun.Text.Substring(0, stat.Offset);
            }
            else if (stat.Offset == 0)
            {
                parent.Children.Insert(statRun.Index, new TsrParameter("パラメータ"));
            }
            else if (stat.Offset == statRun.Text.Length)
            {
                parent.Children.Insert(statRun.Index + 1, new TsrParameter("パラメータ"));
            }
        }

        private void InsertSubScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var word=new C1WordDocument();
            word.Load("C:\\Users\\ey28754\\Desktop\\新規 Microsoft Word 文書.docx");

            var paragraph = new RtfParagraph();

            var section=new RtfSection();
        }
        private void RemoveBullet(C1List target)
        {
            foreach(var item in target.ListItems)
            {
                foreach (var children in item.Children)
                {
                    rtb.Document.Blocks.Insert(target.Index, children.Clone());
                }
            }
            rtb.Document.Blocks.Remove(target);
        }

        private C1List GetC1ListInSelection()
        {
            foreach (var element in rtb.Selection.Blocks)
            {
                var parent = element.Parent;
                if (element.GetType() == typeof(C1List)) return (C1List) element;

                while (parent.GetType() != typeof(C1Document))
                {
                    if (parent.GetType() == typeof(C1List)) return (C1List) parent;
                    parent = parent.Parent;
                }
            }
            return null;
        }
        private void InsertSuperScriptButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void InsertBulletPointButton_Click(object sender, RoutedEventArgs e)
        {
            // 選択範囲内に箇条書きがある場合、最初の箇条書きを削除する。
            var target = GetC1ListInSelection();
            if (target != null)
            {
                RemoveBullet(target);
                return;
            }
            
            var fm = new BulletControlWindow();
            fm.ShowDialog();

            var list = new C1List();
            list.MarkerStyle = fm.MarkerStyle;
            var index = rtb.Selection.Blocks.First().Index;
            var count = rtb.Selection.Blocks.Count();
            for (int i = 0; i < count; i++)
            {
                var element = rtb.Selection.Blocks.First(x => x.Index == index);
                rtb.Document.Blocks.Remove(element);
                var listItem = new C1ListItem();
                listItem.Children.Add(element);
                list.Children.Add(listItem);
            }
            rtb.Document.Blocks.Insert(index, list);
        }
    }
}
