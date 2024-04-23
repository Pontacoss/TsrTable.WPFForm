using C1.WPF.RichTextBox.Documents;

namespace TsrTable.Fake
{
    public static class TsrSentenceFake
    {
        public static TsrRichTextBox GetInitialFake(this TsrRichTextBox tsrRich)
        {
            var para4 = new C1Paragraph();
            var para2 = new C1Paragraph();
            var run2 = new C1Run();
            var para3 = new C1Paragraph();
            var run3 = new C1Run();
            var run4 = new C1Run();

            var run1 = tsrRich.Document.Children[0].Children[0] as C1Run;
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

            tsrRich.Document.Children.Add(para2);
            tsrRich.Document.Children.Add(para3);
            tsrRich.Document.Children.Add(para4);

            return tsrRich;
        }
    }
}
