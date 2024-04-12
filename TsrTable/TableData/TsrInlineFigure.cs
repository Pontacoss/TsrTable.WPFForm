using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Text.Json.Serialization;
using System.Windows.Media.Imaging;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public sealed class TsrInlineFigure : ITsrElement
    {
        public byte[] Binary { get; } = null;

        public double Height { get; }
        public double Width { get; }

        [JsonConstructor]
        public TsrInlineFigure(byte[] binary, double height, double width)
        {
            Binary = binary;
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Ctrl+V で画像を貼り付けた場合にのみ使用
        /// </summary>
        /// <param name="bmp"></param>
        public TsrInlineFigure(BitmapImage bmp)
        {
            Height = bmp.PixelHeight;
            Width = bmp.PixelWidth;
            var stream = bmp.StreamSource;
            Binary = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(Binary, 0, (int)stream.Length);
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new NotImplementedException();
        }

        public void ToExcel(C1XLBook book)
        {
            throw new NotImplementedException();
        }

        public C1TextElement GetRtbInstance()
        {
            return new RtbInlineFigure(Binary, Height, Width);
        }


    }
}
