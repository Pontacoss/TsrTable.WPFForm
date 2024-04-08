using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Linq;
using System.Windows;

namespace TsrTable.RichTextBox
{
    public static class RtbSentenceTools
    {
        public static C1RichTextBox InsertParameter(this C1RichTextBox rtb, string name)
        {
            InsertInlineObject(rtb, new RtbParameter(name));
            return rtb;
        }
        public static C1RichTextBox InsertPostScript(this C1RichTextBox rtb, RtbPostScript postScript, RoutedEventHandler action)
        {
            postScript.SetAction(action);
            InsertInlineObject(rtb, postScript);
            return rtb;
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
            //foreach (var element in rtb.Document.Children)
            //{
            //    if (DeleteElement(element, target)) break;
            //}
            return rtb;
        }

        private static bool DeleteElement(C1TextElement parent, C1TextElement target)
        {
            foreach (var child in parent.Children)
            {
                if (child == target)
                {
                    target.Remove();
                    //parent.Children.Remove(target);
                    return true;
                }
                else
                   if (DeleteElement(child, target)) return true;
            }
            return false;
        }

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
        public static C1RichTextBox InsertBullet(this C1RichTextBox rtb, C1.WPF.RichTextBox.Documents.TextMarkerStyle style)
        {
            var index = rtb.Selection.Blocks.First().Index;
            var bullet = new C1List() { MarkerStyle = style }; // new RtbBullet(rtb, style);

            var count = rtb.Selection.Blocks.Count();
            for (int i = 0; i < count; i++)
            {
                var element = rtb.Selection.Blocks.First(x => x.Index == index);
                rtb.Document.Blocks.Remove(element);
                var item = new C1ListItem();
                item.Children.Add(element);
                bullet.Children.Add(item);
            }

            rtb.Document.Blocks.Insert(index, bullet);
            return rtb;
        }

        public static C1RichTextBox InsertSubScript(this C1RichTextBox rtb, string baseScriptString, string subScriptString)
        {
            InsertInlineObject(rtb, new RtbSuperScript(baseScriptString, subScriptString));
            return rtb;
        }

        public static C1RichTextBox InsertSuperScript(this C1RichTextBox rtb, string baseScriptString, string superScriptString)
        {
            InsertInlineObject(rtb, new RtbSuperScript(baseScriptString, superScriptString));
            return rtb;
        }

        public static C1RichTextBox InsertSubTitle(this C1RichTextBox rtb, string text)
        {
            if (text != null)
            {
                InsertInlineObject(rtb, new RtbSubTitle(text));
            }
            return rtb;
        }

        /// <summary>
        /// 現在のキャレット位置にC1Inlineオブジェクトを挿入する。
        /// </summary>
        /// <param name="element"></param>
        private static void InsertInlineObject(C1RichTextBox rtb, C1TextElement element)
        {
            C1TextRange selectText = rtb.Selection;
            var stat = selectText.Start;
            var statRun = stat.Element as C1Run;
            if (statRun == null) return;

            var parent = statRun.Parent;
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
            foreach (var item in target.Children)
            {
                for (var j = 0; j < item.Children.Count; j++)
                {
                    item.Children[j].UnGroup();
                }
                //foreach (var child in item.Children)
                //{
                //    child.UnGroup();
                //    //rtb.Document.Blocks.Insert(target.Index, child.Clone());
                //}
            }
            target.UnGroup();
            //rtb.Document.Blocks.Remove(target);
        }
    }
}
