using C1.WPF.RichTextBox.Documents;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public static class TsrSentenceTools
    {
        public static C1TextElement ToRtb(this ITsrElement element)
        {
            if (element == null) return null;

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
                else if (ui.Content is System.Windows.Controls.Image || ui.Content is BitmapImage)
                    return new TsrInlineFigure(ui);
                throw new NotImplementedException();
            }
            else
                throw new NotImplementedException();

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

        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
