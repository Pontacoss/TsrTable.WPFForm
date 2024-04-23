using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TsrTable.RichTextBox;
using TsrTable.TsrElement;
using TsrTable.UserControls;

namespace TsrTable
{
    public sealed class TsrRichTextBox : C1RichTextBox
    {
        #region Constractor
        public TsrRichTextBox()
        {
            this.FontSize = 10.5;
            this.ViewMode = TextViewMode.Draft;
            this.Zoom = 1.3;
            this.HideSelection = false;
            this.DefaultParagraphMargin = new Thickness(0, 0, 0, 0);
        }
        #endregion

        public string Serialize(bool? indented = false)
        {
            var sentence = this.Document.ToTsr();
            var options = new JsonSerializerOptions
            {
                WriteIndented = indented == true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            return JsonSerializer.Serialize(sentence, options);
        }

        public void Deserialize(string text)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            var tsrSentence = JsonSerializer.Deserialize<ITsrBlock>(text, options);

            this.Document.Children.Clear();

            foreach (var child in tsrSentence.Children)
                this.Document.Children.Add(child.ToRtb());

            foreach (var postScript in this.Document.EnumerateSubtree().OfType<IRtbPostScript>())
            {
                postScript.SetAction(PostScriptInnerButton_Click);
            }
        }

        /// <summary>
        /// オブジェクト編集用のダイアログウィンドウを取得
        /// 動きはするけど超強引
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        internal Window GetEditorWindow(UserControl editor)
        {
            // TsrRichTextBox
            Type type = Window.GetWindow(this).GetType();
            var window = Activator.CreateInstance(type) as Window;

            var content = new StackPanel()
            {
                Height = editor.Height,
                Width = editor.Width,
            };
            content.Children.Add(editor);

            window.Content = content;
            window.Height = editor.Height + 42;
            window.Width = editor.Width + 20;
            window.Title = editor.Name;

            return window;
        }

        private void PostScriptInnerButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var postScript = button.Tag as C1TextElement;

            var editor = new TsrPostScriptEditor(postScript, PostScriptInnerButton_Click);

            //todo PostScriptWindowを×ボタンで消した場合の挙動(キャンセル)が書けていない。
            if (this.GetEditorWindow(editor).ShowDialog() == true)
            {
                var index = postScript.Index;
                postScript.Parent.Children.Insert(index, editor.NewValue);
            }
            postScript.Remove();
        }

        /// <summary>
        /// RichTextBoxの現在のカーソル位置にC1Inlineオブジェクトを挿入する拡張メソッド。
        /// </summary>
        /// <param name="element"></param>
        private void InsertInlineObject(C1TextElement element)
        {
            C1TextRange selectText = this.Selection;
            var stat = selectText.Start;
            var statRun = stat.Element as C1Run;
            if (statRun == null) return;

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

            this.Selection = new C1TextRange(new C1TextPointer(statRun, 0));
        }

        /// <summary>
        ///  RichTextBoxの現在のカーソル位置にパラメータを挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public void InsertParameter(string name)
        {
            this.InsertInlineObject(new RtbParameter(name));
        }

        /// <summary>
        /// RichTextBoxの現在のカーソル位置にをSubTitle挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns></returns>
        public void InsertSubTitle()
        {
            var editor = new TsrSubTitleEditor();
            GetEditorWindow(editor).ShowDialog();
            if (string.IsNullOrEmpty(editor.Text)) return;
            InsertInlineObject(new RtbSubTitle(editor.Text));
        }

        /// <summary>
        /// RichTextBoxの現在のカーソル位置に上付き文字を挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="baseScriptString"></param>
        /// <param name="superScriptString"></param>
        /// <returns></returns>
        public void InsertSuperSubScript()
        {
            var editor = new TsrSuperSubScriptEditor();
            GetEditorWindow(editor).ShowDialog();

            if (editor.BaseScriptString == string.Empty) return;
            else if (editor.SuperScriptString != string.Empty)
            {
                this.InsertInlineObject(
                    new RtbSuperScript(
                        editor.BaseScriptString,
                        editor.SuperScriptString));
            }
            else if (editor.SubScriptString != string.Empty)
            {
                this.InsertInlineObject(
                    new RtbSubScript(
                        editor.BaseScriptString,
                        editor.SubScriptString));
            }
        }

        /// <summary>
        ///  RichTextBoxの現在のカーソル位置に追記を挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="postScript"></param>
        /// <returns></returns>
        public void InsertPostScript()
        {
            var editor = new TsrPostScriptEditor(PostScriptInnerButton_Click);
            var win = GetEditorWindow(editor);
            if (win.ShowDialog() == false) return;

            this.InsertInlineObject(editor.NewValue);
        }



        /// <summary>
        /// RichTextBoxの中から指定したオブジェクトを探して削除する。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public void DeleteTextElement(C1TextElement target)
        {
            target.Remove();
        }

        /// <summary>
        /// RichTextBoxの現在の選択範囲に対して取消線のON/OFFを切り替える拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns></returns>
        public void InsertStrikethrough()
        {
            var selection = this.Selection;
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

        /// <summary>
        /// RichTextBoxの現在の選択範囲を箇条書きにする拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public void InsertBullet()
        {
            if (RemoveBullet()) return;
            var selector = new TsrBulletMarkerSelector();
            GetEditorWindow(selector).ShowDialog();

            var style = selector.MarkerStyle;

            var parent = this.Selection.Blocks.First().Parent;
            var index = this.Selection.Blocks.First().Index;
            var bullet = new C1List()
            {
                MarkerStyle = style,
                Margin = new System.Windows.Thickness(40, 0, 0, 0)
            };

            var count = this.Selection.Blocks.Count();

            for (int i = 0; i < count; i++)
            {
                var element = this.Selection.Blocks.First(x => x.Index == index);
                parent.Children.Remove(element);
                var item = new C1ListItem();
                item.Children.Add(element);
                bullet.Children.Add(item);
            }

            parent.Children.Insert(index, bullet);

        }

        private bool RemoveBullet()
        {
            var target = this.GetBulletInSelection();
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

        private C1List GetBulletInSelection()
        {
            foreach (var element in this.Selection.Blocks)
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

        public void ViewModeChange()
        {
            this.ViewMode = this.ViewMode == TextViewMode.Print ?
                TextViewMode.Draft : TextViewMode.Print;
        }

        public void ZoomIn()
        {
            if (this.Zoom >= 3) return;
            this.Zoom += 0.1;
        }

        public void ZoomOut()
        {
            if (this.Zoom <= 0.5) return;
            this.Zoom -= 0.1;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //    if ((Control.ModifierKeys & System.Windows.Forms.Keys.Control) == System.Windows.Forms.Keys.Control)
            //    {
            //        if (e.Delta > 0)
            //        {
            //            if (this.Zoom <= 3) this.Zoom += 0.1;
            //        }
            //        else
            //        {
            //            if (this.Zoom >= 0.5) this.Zoom -= 0.1;
            //        }
            //    }
            //    else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            //    {
            //        if (e.Delta > 0)
            //        {

            //        }
            //        else
            //        {
            //            if (this.HorizontalOffset == 0) return;
            //        }
            //        HorizontalOffset += e.Delta / 5;
            //    }
            //    else return;
        }
    }
}
