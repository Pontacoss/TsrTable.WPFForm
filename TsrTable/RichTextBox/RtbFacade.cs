using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Linq;
using System.Windows;
using TsrTable.UserControls;

namespace TsrTable.RichTextBox
{
    public static class RtbFacade
    {
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
        public static C1RichTextBox InsertPostScript(this C1RichTextBox rtb, C1TextElement postScript)
        {
            return rtb.InsertInlineObject(postScript);
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

        private static bool DeleteElement(C1TextElement parent, C1TextElement target)
        {
            foreach (var child in parent.Children)
            {
                if (child == target)
                {
                    target.Remove();
                    return true;
                }
                else
                   if (DeleteElement(child, target)) return true;
            }
            return false;
        }

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

        public static bool RemoveBullet(C1RichTextBox rtb)
        {
            var target = GetBulletInSelection(rtb);
            if (target != null)
            {
                RemoveBullet(rtb, target);
                return true;
            }
            return false;
        }

        /// <summary>
        /// RichTextBoxの現在の選択範囲を箇条書きにする拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static C1RichTextBox InsertBullet(this C1RichTextBox rtb,
            C1.WPF.RichTextBox.Documents.TextMarkerStyle style)
        {
            var parent = rtb.Selection.Blocks.First().Parent;
            var index = rtb.Selection.Blocks.First().Index;
            var bullet = new C1List() { MarkerStyle = style, Margin = new System.Windows.Thickness(40, 0, 0, 0) };
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

        /// <summary>
        /// RichTextBoxの現在のカーソル位置に下付き文字を挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="baseScriptString"></param>
        /// <param name="subScriptString"></param>
        /// <returns></returns>
        public static C1RichTextBox InsertSubScript(this C1RichTextBox rtb, string baseScriptString, string subScriptString)
        {
            return rtb.InsertInlineObject(new RtbSubScript(baseScriptString, subScriptString));
        }

        /// <summary>
        /// RichTextBoxの現在のカーソル位置に上付き文字を挿入する拡張メソッド。
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="baseScriptString"></param>
        /// <param name="superScriptString"></param>
        /// <returns></returns>
        public static C1RichTextBox InsertSuperScript(this C1RichTextBox rtb, string baseScriptString, string superScriptString)
        {
            ;
            return rtb.InsertInlineObject(new RtbSuperScript(baseScriptString, superScriptString));

        }

        public static C1RichTextBox InsertSubTitle(this C1RichTextBox rtb, string text)
        {
            if (text != null)
            {
                InsertInlineObject(rtb, new RtbSubTitle(text));
            }
            return rtb;
        }
        public static C1RichTextBox InsertSubTitle(this C1RichTextBox rtb, Window window)
        {
            var blankWindow = new window(new TsrSubTitleEditWindow());
            if (text != null)
            {
                InsertInlineObject(rtb, new RtbSubTitle(text));
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

        private static void RemoveBullet(C1RichTextBox rtb, C1List target)
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
        }
    }
}
