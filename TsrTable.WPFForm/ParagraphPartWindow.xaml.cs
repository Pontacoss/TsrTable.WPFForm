﻿using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using TsrTable.RichTextBox;
using TsrTable.RichTextBox.TsrElement;
using TsrTable.UserControls;

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


        }


        private void InsertParameterButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.InsertParameter("パラメータ");
        }

        private void InsertSubScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var fm = new SuperSubScriptWindow();
            fm.ShowDialog();

            if (fm.BaseScriptString == string.Empty) return;
            if (fm.SuperScriptString != string.Empty)
            {
                rtb.InsertSuperScript(fm.BaseScriptString, fm.SuperScriptString);
            }
            else if (fm.SubScriptString != string.Empty)
            {
                rtb.InsertSubScript(fm.BaseScriptString, fm.SubScriptString);
            }
            else return;
        }

        private void InsertStrikethroughButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.InsertStrikethrough();
        }

        private void InsertBulletPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (RtbFacade.RemoveBullet(rtb)) return;
            var fm = new BulletControlWindow();
            fm.ShowDialog();
            rtb.InsertBullet(fm.MarkerStyle);
        }

        private void PostScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var IsInline = sender is RtbInlinePostScript;
            var fm = new PostScriptWindow(PostScriptInnerButton_Click);
            if (fm.ShowDialog() == true)
                rtb.InsertPostScript(fm.NewValue);

        }
        private void PostScriptInnerButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var postScript = button.Tag as C1TextElement;

            var fm = new PostScriptWindow(postScript, PostScriptInnerButton_Click);

            // todo PostScriptWindowを×ボタンで消した場合の挙動(キャンセル)が書けていない。
            if (fm.ShowDialog() == true)
            {
                var index = postScript.Index;
                postScript.Parent.Children.Insert(index, fm.NewValue);
            }
            postScript.Remove();
        }

        private void SubTitleButton_Click(object sender, RoutedEventArgs e)
        {
            var blankWindow = new BlankWindow(new TsrSubTitleEditWindow());


            RtbFacade.InsertSubTitle(blankWindow);

            //var fm = new SubTitleWindow();
            //fm.ShowDialog();
            //rtb.InsertSubTitle(fm.Text);
        }

        private void EditableChangeButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.IsReadOnly = rtb.IsReadOnly != true;
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            //var word = new C1WordDocument();
            //word.Load("C:\\Users\\ey28754\\Desktop\\新規 Microsoft Word 文書.docx");
            var sentence = rtb.Document.ToTsr();

            var options = new JsonSerializerOptions
            {
                WriteIndented = IndentedCheckBox.IsChecked == true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            var serializer = JsonSerializer.Serialize(sentence, options);

            JsonTextBox.Text = serializer;
            JsonTextCountTextBlock.Text = serializer.Count().ToString();

            var tsrSentence = JsonSerializer.Deserialize<TsrSentence>(serializer, options);

            rtb.Document.Children.Clear();

            foreach (var child in tsrSentence.Children)
                rtb.Document.Children.Add(child.ToRtb());

            foreach (var postScript in rtb.Document.EnumerateSubtree().OfType<IRtbPostScript>())
            {
                postScript.SetAction(PostScriptInnerButton_Click);
            }

            // 取消線のリスト化
            var list = SeekStrikethroughRecrusion(rtb.Document);

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



        private void ExtractButton_Click(object sender, RoutedEventArgs e)
        {
            var tsrParagraph = new TsrDocument(rtb.Document);
        }
    }
}
