using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TsrTable.RichTextBox;
using TsrTable.RichTextBox.TsrElement;
using TsrTable.UserControls;

namespace TsrTable
{
    public static class TsrFacade
    {
        public static Window EditWindow { get; set; }
        //{
        //    get
        //    {
        //        if (EditWindow == null)
        //            throw new NotImplementedException();
        //        return EditWindow;
        //    }
        //    set
        //    {
        //        //if (!(value.Content is Panel))
        //        //    throw new NotImplementedException();
        //        EditWindow = value;
        //    }
        //}

        public static string Serialize(C1RichTextBox rtb, bool? indented = false)
        {
            var sentence = rtb.Document.ToTsr();
            var options = new JsonSerializerOptions
            {
                WriteIndented = indented == true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            return JsonSerializer.Serialize(sentence, options);
        }

        public static C1RichTextBox Deserialize(this C1RichTextBox rtb, string text)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            var tsrSentence = JsonSerializer.Deserialize<ITsrBlock>(text, options);

            rtb.Document.Children.Clear();

            foreach (var child in tsrSentence.Children)
                rtb.Document.Children.Add(child.ToRtb());

            foreach (var postScript in rtb.Document.EnumerateSubtree().OfType<IRtbPostScript>())
            {
                postScript.SetAction(PostScriptInnerButton_Click);
            }

            return rtb;
        }

        /// <summary>
        ///  RichTextBoxの現在のカーソル位置にパラメータを挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static C1RichTextBox InsertParameter(this C1RichTextBox rtb, string name)
        {
            return rtb.InsertInlineObject(new RtbParameter(name));
        }

        /// <summary>
        ///  RichTextBoxの現在のカーソル位置に追記を挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="postScript"></param>
        /// <returns></returns>
        public static C1RichTextBox InsertPostScript(this C1RichTextBox rtb)
        {
            var editor = new TsrPostScriptEditWindow(PostScriptInnerButton_Click);

            if (GetEditWindow(editor).ShowDialog() == false) return rtb;

            return rtb.InsertInlineObject(editor.NewValue);
        }

        private static void PostScriptInnerButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var postScript = button.Tag as C1TextElement;

            var editor = new TsrPostScriptEditWindow(postScript, PostScriptInnerButton_Click);

            //todo PostScriptWindowを×ボタンで消した場合の挙動(キャンセル)が書けていない。
            if (GetEditWindow(editor).ShowDialog() == true)
            {
                var index = postScript.Index;
                postScript.Parent.Children.Insert(index, editor.NewValue);
            }
            postScript.Remove();
        }

        /// <summary>
        /// RichTextBoxの中から指定したオブジェクトを探して削除する。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static C1RichTextBox DeleteTextElement(this C1RichTextBox rtb, C1TextElement target)
        {
            target.Remove();
            return rtb;
        }

        //private static bool DeleteElement(C1TextElement parent, C1TextElement target)
        //{
        //    foreach (var child in parent.Children)
        //    {
        //        if (child == target)
        //        {
        //            target.Remove();
        //            return true;
        //        }
        //        else
        //           if (DeleteElement(child, target)) return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// RichTextBoxの現在の選択範囲に対して取消線のON/OFFを切り替える拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns></returns>
        public static C1RichTextBox InsertStrikethrough(this C1RichTextBox rtb)
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
            return rtb;
        }

        /// <summary>
        /// RichTextBoxの現在の選択範囲を箇条書きにする拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static C1RichTextBox InsertBullet(this C1RichTextBox rtb)
        {
            if (RemoveBullet(rtb)) return rtb;
            var selector = new TsrBulletMarkerSelector();
            GetEditWindow(selector).ShowDialog();

            var style = selector.MarkerStyle;

            var parent = rtb.Selection.Blocks.First().Parent;
            var index = rtb.Selection.Blocks.First().Index;
            var bullet = new C1List()
            {
                MarkerStyle = style,
                Margin = new System.Windows.Thickness(40, 0, 0, 0)
            };

            var count = rtb.Selection.Blocks.Count();

            for (int i = 0; i < count; i++)
            {
                var element = rtb.Selection.Blocks.First(x => x.Index == index);
                parent.Children.Remove(element);
                var item = new C1ListItem();
                item.Children.Add(element);
                bullet.Children.Add(item);
            }

            parent.Children.Insert(index, bullet);
            return rtb;
        }

        private static bool RemoveBullet(C1RichTextBox rtb)
        {
            var target = GetBulletInSelection(rtb);
            if (target != null)
            {
                var counter = 1;
                foreach (var item in target.Children)
                {
                    foreach (var child in item.Children)
                    {
                        target.Parent.Children.Insert(target.Index + counter, child.Clone());
                        counter++;
                    }
                }
                target.Parent.Children.Remove(target);
                return true;
            }
            return false;
        }

        private static C1List GetBulletInSelection(C1RichTextBox rtb)
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

        /// <summary>
        /// RichTextBoxの現在のカーソル位置に上付き文字を挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="baseScriptString"></param>
        /// <param name="superScriptString"></param>
        /// <returns></returns>
        public static C1RichTextBox InsertSuperSubScript(this C1RichTextBox rtb)
        {
            var editor = new TsrSuperSubScriptEditWindow();
            GetEditWindow(editor).ShowDialog();

            if (editor.BaseScriptString == string.Empty)
                return rtb;
            else if (editor.SuperScriptString != string.Empty)
            {
                return rtb.InsertInlineObject(
                    new RtbSuperScript(
                        editor.BaseScriptString,
                        editor.SuperScriptString));
            }
            else if (editor.SubScriptString != string.Empty)
            {
                return rtb.InsertInlineObject(
                    new RtbSubScript(
                        editor.BaseScriptString,
                        editor.SubScriptString));
            }
            else
                return rtb;
        }

        /// <summary>
        /// オブジェクト編集用のダイアログウィンドウを取得
        /// </summary>
        /// <param name="userControl"></param>
        /// <returns></returns>
        internal static Window GetEditWindow(UserControl userControl)
        {
            Type type = EditWindow.GetType();
            var window = Activator.CreateInstance(type) as Window;

            var content = window.Content as Panel;
            content.Children.Clear();
            content.Children.Add(userControl);

            window.Height = userControl.Height + 42;
            window.Width = userControl.Width + 20;
            window.Title = userControl.Name;

            return window;
        }

        /// <summary>
        /// RichTextBoxの現在のカーソル位置にをSubTitle挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns></returns>
        public static C1RichTextBox InsertSubTitle(this C1RichTextBox rtb)
        {
            var editor = new TsrSubTitleEditWindow();
            GetEditWindow(editor).ShowDialog();

            if (editor.Text != null)
            {
                InsertInlineObject(rtb, new RtbSubTitle(editor.Text));
            }
            return rtb;
        }

        /// <summary>
        /// RichTextBoxの現在のカーソル位置にC1Inlineオブジェクトを挿入する拡張メソッド。
        /// </summary>
        /// <param name="element"></param>
        private static C1RichTextBox InsertInlineObject(this C1RichTextBox rtb, C1TextElement element)
        {
            C1TextRange selectText = rtb.Selection;
            var stat = selectText.Start;
            var statRun = stat.Element as C1Run;
            if (statRun == null) return rtb;

            var parent = statRun.Parent;
            // C1Runの途中にC1TextElementを挿入する場合
            // 新たなC1Runを生成して、挿入箇所から後ろの部分を入れる。
            if (0 < stat.Offset && stat.Offset < statRun.Text.Length)
            {
                parent.Children.Insert(statRun.Index + 1, element);
                parent.Children.Insert(statRun.Index + 2, new C1Run()
                {
                    Text = statRun.Text.Substring(stat.Offset, statRun.Text.Length - stat.Offset)
                });
                statRun.Text = statRun.Text.Substring(0, stat.Offset);
            }
            // 頭に挿入する場合
            else if (stat.Offset == 0)
            {
                parent.Children.Insert(statRun.Index, element);
            }
            // 最後に入れる場合
            else if (stat.Offset == statRun.Text.Length)
            {
                parent.Children.Insert(statRun.Index + 1, element);
                parent.Children.Insert(statRun.Index + 2, new C1Run());
            }

            rtb.Selection = new C1TextRange(new C1TextPointer(statRun, 0));

            return rtb;
        }

        /// <summary>
        /// ITsrElementを子要素も含めて対応するC1TextElementに変換する。
        /// </summary>
        /// <param name="element"></param>
        /// <returns>C1TextElement(Nullable)</returns>
        public static C1TextElement ToRtb(this ITsrElement element)
        {
            C1TextElement rtbInstance = element.GetRtbInstance();
            if (element is ITsrBlock block)
            {
                foreach (var child in block.Children)
                {
                    var rtbElement = child.ToRtb();
                    if (rtbElement != null)
                    {
                        rtbInstance.Children.Add(rtbElement);
                    }
                }
            }
            return rtbInstance;
        }

        /// <summary>
        /// C1TextElementを子要素も含めて対応するITsrElementに変換する。
        /// </summary>
        /// <param name="element"></param>
        /// <returns>ITsrElement(Nullable)</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ITsrElement ToTsr(this C1TextElement element)
        {
            ITsrElement tsrElement;
            if (element is IRtbElement rtbe)
                tsrElement = rtbe.GetTsrInstance();
            else if (element is C1Document)
                tsrElement = new TsrSentence();
            else if (element is C1Paragraph)
                tsrElement = new TsrParagraph();
            else if (element is C1Span)
                tsrElement = new TsrSpan();
            else if (element is C1Run run)
            {
                if (run.TextDecorations == C1TextDecorations.Strikethrough)
                {
                    tsrElement = new TsrStrikethrough(run.Text);
                }
                else
                {
                    if (string.IsNullOrEmpty(run.Text)) return null;
                    tsrElement = new TsrRun(run.Text);
                }
            }
            else if (element is C1InlineUIContainer ui)
            {
                if (ui.Content is Button)
                    return null;
                else if (ui.Content is System.Windows.Controls.Image || ui.Content is System.Windows.Media.Imaging.BitmapImage)
                    tsrElement = new TsrInlineFigure(ui.Content as BitmapImage);
                else
                    throw new NotImplementedException();
            }
            else if (element is C1List c1List)
            {
                tsrElement = new TsrBullet(c1List.MarkerStyle);
            }
            else if (element is C1ListItem)
            {
                tsrElement = new TsrBulletItem();
            }
            else
            {
                MessageBox.Show("未実装 例外  ", element.ToString());
                return null;
            }

            if (tsrElement is ITsrBlock block)
            {
                foreach (var child in element.Children)
                {
                    var tsrChild = child.ToTsr();
                    if (tsrChild != null)
                    {
                        block.Children.Add(tsrChild);
                    }
                }
            }
            return tsrElement;
        }

        //private static BitmapImage ToBitmapImage(this Bitmap bitmap)
        //{
        //    using (var memory = new MemoryStream())
        //    {
        //        bitmap.Save(memory, ImageFormat.Png);
        //        memory.Position = 0;

        //        var bitmapImage = new BitmapImage();
        //        bitmapImage.BeginInit();
        //        bitmapImage.StreamSource = memory;
        //        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //        bitmapImage.EndInit();
        //        bitmapImage.Freeze();

        //        return bitmapImage;
        //    }
        //}
    }
}

