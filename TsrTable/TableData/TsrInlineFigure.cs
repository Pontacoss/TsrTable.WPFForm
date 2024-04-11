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

        public TsrInlineFigure(C1InlineUIContainer container)
        {
            //if (container.Content is BitmapImage bmp)
            //{
            var bmp = container.Content as BitmapImage;
            var stream = bmp.StreamSource;
            Binary = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(Binary, 0, (int)stream.Length);
            //}
            //else if (container.Content is System.Windows.Controls.Image img)
            //{
            //    //todo 画像のデータ取り込みが出来ない。
            //    BitmapImage source = img.Source as BitmapImage;
            //    using (var fs = source.StreamSource)
            //    {
            //        Binary = new byte[fs.Length];
            //        fs.Write(Binary, 0, (int)fs.Length);
            //        var writer = new BinaryWriter(fs);
            //        writer.Write(Binary);
            //    }
            //}
            Height = container.Height.Value;
            Width = container.Width.Value;
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
