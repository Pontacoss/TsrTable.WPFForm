using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json.Serialization;
using System.Windows.Media.Imaging;

namespace TsrTable.TableData
{
    public sealed class TsrInlineFigure : ITsrElement
    {
        public byte[] Binary { get; } = null;
        public C1Length ImageHeight { get; }
        public C1Length ImageWidth { get; }

        [JsonConstructor]
        public TsrInlineFigure(byte[] binary, C1Length imageHeight, C1Length imageWidth)
        {
            Binary = binary;
            ImageHeight = imageHeight;
            ImageWidth = imageWidth;
        }

        public TsrInlineFigure(C1InlineUIContainer container)
        {
            if (container.Content is BitmapImage bmp)
            {
                var stream = bmp.StreamSource;
                Binary = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(Binary, 0, (int)stream.Length);
            }
            else if (container.Content is System.Windows.Controls.Image img)
            {
                //todo 画像のデータ取り込みが出来ない。
                BitmapImage source = img.Source as BitmapImage;
                using (var fs = source.StreamSource)
                {
                    Binary = new byte[fs.Length];
                    fs.Write(Binary, 0, (int)fs.Length);
                    var writer = new BinaryWriter(fs);
                    writer.Write(Binary);
                }
            }
            ImageHeight = container.Height;
            ImageWidth = container.Width;
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
            // バイナリデータからBitmp経由でBitmapImageに変換
            using (var ms = new MemoryStream(Binary))
            {
                var bmp = new Bitmap(ms);
                var bmpImage = bmp.ToBitmapImage();

                return new C1InlineUIContainer()
                {
                    Content = new System.Windows.Controls.Image()
                    {
                        Source = bmpImage,
                    },
                    Height = ImageHeight,
                    Width = ImageWidth,
                };
            }
        }


    }
}
