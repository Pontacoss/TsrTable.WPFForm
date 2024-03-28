using C1.WPF.RichTextBox.Documents;
using System;
using System.Windows.Media.Imaging;

namespace TsrTable.TableData
{
    internal static class TsrSentenceTools
    {
        public static ITsrElement ConvertC1ToTsr(C1TextElement element)
        {
            if (element is C1Paragraph c1p)
            {
                return new TsrParagraph(c1p);
            }
            else if (element is C1Run run)
            {
                if (run.TextDecorations == C1TextDecorations.Strikethrough)
                {
                    return new TsrStrikethrough(run.Text);
                }
                else
                {
                    return new TsrRun(run.Text);
                }
            }
            else if (element is C1InlineUIContainer container)
            {
                var bitmap = container.Content as BitmapImage;
                if (bitmap != null)
                {
                    return new TsrInlineFigure(container);
                }
                throw new NotImplementedException();
            }
            else
                throw new NotImplementedException();
        }
    }
}
