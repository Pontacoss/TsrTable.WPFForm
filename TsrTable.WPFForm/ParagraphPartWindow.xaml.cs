using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TsrTable.RichTextBox;
using TsrTable.TableData;

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
            rtb.ViewMode = TextViewMode.Draft;
            rtb.Zoom = 1.3;
            rtb.HideSelection = false;
            rtb.DefaultParagraphMargin = new Thickness(0, 0, 0, 0);
        }


        private void InsertParameterButton_Click(object sender, RoutedEventArgs e)
        {
            InsertInlineObject(new TsrParameter("パラメータ"));
        }

        private void InsertSubScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var fm = new SuperSubScriptWindow();
            fm.ShowDialog();

            if (fm.BaseScriptString == string.Empty) return;
            if (fm.SuperScriptString != string.Empty)
            {
                InsertInlineObject(new RtbSuperScript(fm.BaseScriptString, fm.SuperScriptString));
            }
            else if (fm.SubScriptString != string.Empty)
            {
                InsertInlineObject(new RtbSubScript(fm.BaseScriptString, fm.SuperScriptString));
            }
            else return;
        }

        private void RemoveBullet(C1List target)
        {
            foreach (var item in target.ListItems)
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
                if (element.GetType() == typeof(C1List)) return (C1List)element;

                while (parent.GetType() != typeof(C1Document))
                {
                    if (parent.GetType() == typeof(C1List)) return (C1List)parent;
                    parent = parent.Parent;
                }
            }
            return null;
        }
        private void InsertSuperScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var selection = rtb.Selection;
            if (selection.TextDecorations == C1TextDecorations.Strikethrough)
            {
                selection.TextDecorations = null;
            }
            else
            {
                selection.TextDecorations = C1TextDecorations.Strikethrough;
                selection.TextDecorations[0].LocationOffset = 0;
                selection.TextDecorations[0].Thickness = 0.1;
            }
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

            var index = rtb.Selection.Blocks.First().Index;

            var bullet = new RtbBullet(rtb, index, fm.MarkerStyle);


            rtb.Document.Blocks.Insert(index, bullet);
        }
        /// <summary>
        /// 現在のキャレット位置にC1Inlineオブジェクトを挿入する。
        /// </summary>
        /// <param name="element"></param>
        private void InsertInlineObject(C1Inline element)
        {
            C1TextRange selectText = rtb.Selection;
            var stat = selectText.Start;
            var statRun = stat.Element as C1Run;
            if (statRun == null) return;

            var parent = statRun.Parent; // as C1Paragraph;
            if (0 < stat.Offset && stat.Offset < statRun.Text.Length)
            {
                parent.Children.Insert(statRun.Index + 1, element);
                parent.Children.Insert(statRun.Index + 2, new C1Run()
                {
                    Text = statRun.Text.Substring(stat.Offset, statRun.Text.Length - stat.Offset)
                });
                statRun.Text = statRun.Text.Substring(0, stat.Offset);
            }
            else if (stat.Offset == 0)
            {
                parent.Children.Insert(statRun.Index, element);
            }
            else if (stat.Offset == statRun.Text.Length)
            {
                parent.Children.Insert(statRun.Index + 1, element);
                parent.Children.Insert(statRun.Index + 2, new C1Run());
            }
        }

        private void EditableChangeButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.IsReadOnly = rtb.IsReadOnly != true;
        }

        private void PostScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var fm = new PostScriptWindow();
            fm.ShowDialog();

            if (fm.Text.Length > 0)
            {
                InsertInlineObject(
                    new RtbPostScript(
                        fm.Text,
                        fm.Color,
                        PostScriptInnerButton_Click));
            }
        }

        private void PostScriptInnerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
            {
                throw new NotImplementedException();
            }
            if (!(button.Tag is RtbPostScript postScript))
            {
                throw new NotImplementedException();
            }
            var fm = new PostScriptWindow(postScript.Text, postScript.Color);
            fm.ShowDialog();
            postScript.EditText(fm.Text, fm.Color);
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            //var word = new C1WordDocument();
            //word.Load("C:\\Users\\ey28754\\Desktop\\新規 Microsoft Word 文書.docx");


            var sentence = new TsrSentence(rtb.Document);

            rtb.Document.Children.Clear();

            rtb.Document.Children.Add(sentence.ToRtb());



            var list = new List<C1Run>();
            foreach (var element in rtb.Document.Children)
            {
                if (element is C1Run)
                {
                    if (element.TextDecorations == C1TextDecorations.Strikethrough)
                    {
                        list.Add((C1Run)element);
                    }
                }
                else
                {
                    list.AddRange(SeekStrikethroughRecrusion(element));
                }
            }
        }

        private List<C1Run> SeekStrikethroughRecrusion(C1TextElement element)
        {
            var list = new List<C1Run>();
            foreach (var child in element.Children)
            {

                if (child is C1Run)
                {
                    if (child.TextDecorations == C1TextDecorations.Strikethrough)
                    {
                        list.Add((C1Run)child);
                    }
                }
                else
                {
                    list.AddRange(SeekStrikethroughRecrusion(child));
                }
            }
            return list;
        }

        private void SubTitleButton_Click(object sender, RoutedEventArgs e)
        {
            var fm = new PostScriptWindow();
            fm.ShowDialog();

            if (fm.Text.Length > 0)
            {
                InsertInlineObject(new TsrSubTitle(fm.Text));
            }
        }

        private void ExtractButton_Click(object sender, RoutedEventArgs e)
        {
            var tsrParagraph = new TsrDocument(rtb.Document);
        }
    }
}
