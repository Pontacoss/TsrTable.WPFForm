using C1.WPF.RichTextBox.Documents;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public static class TsrSentenceTools
    {
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
