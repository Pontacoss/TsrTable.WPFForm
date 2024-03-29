using C1.WPF.RichTextBox.Documents;
using System;
using System.Windows.Controls;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public static class TsrSentenceTools
    {
        public static C1TextElement ToRtb(this ITsrElement element)
        {
            if (element == null) return null;

            C1TextElement textElement = element.GetRtbInstance();
            if (element is ITsrBlock block)
            {
                foreach (var child in block.Children)
                {
                    textElement.Children.Add(child.ToRtb());
                }
            }
            return textElement;
        }

        public static ITsrElement ToTsr(this C1TextElement element)
        {
            ITsrElement tsrElement;
            if (element is C1Paragraph paragraph)
                tsrElement = new TsrParagraph(paragraph);
            else if (element is C1Run run)
            {
                if (run.TextDecorations == C1TextDecorations.Strikethrough)
                {
                    tsrElement = new TsrStrikethrough(run.Text);
                }
                else
                {
                    tsrElement = new TsrRun(run.Text);
                }
            }
            else if (element is IRtbElement rtbe)
                tsrElement = rtbe.GetTsrInstance();
            else if (element is C1InlineUIContainer ui)
            {
                if (ui.Content is Button)
                    return null;
                throw new NotImplementedException();
            }
            else
                throw new NotImplementedException();

            if (tsrElement is ITsrBlock block)
            {
                foreach (var child in element.Children)
                {
                    block.Children.Add(child.ToTsr());
                }
            }
            return tsrElement;
        }
    }
}
