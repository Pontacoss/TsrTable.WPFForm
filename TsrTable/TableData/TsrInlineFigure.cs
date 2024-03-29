using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace TsrTable.TableData
{
    public sealed class TsrInlineFigure : ITsrElement
    {
        public byte[] Binary { get; } = null;
        public C1Length ImageHeight { get; }
        public C1Length ImageWidth { get; }

        public TsrInlineFigure(C1InlineUIContainer container)
        {
            var bmp = container.Content as BitmapImage;
            var stream = bmp.StreamSource;
            Binary = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(Binary, 0, (int)stream.Length);

            ImageHeight = container.Height;
            ImageWidth = container.Width;
        }

        public RtfObject ToWord()
        {
            throw new System.NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new System.NotImplementedException();
        }

        public void ToExcel(C1XLBook book)
        {
            throw new System.NotImplementedException();
        }

        public C1TextElement GetRtbInstance()
        {
            // バイナリデータから画像をファイルに書き出し
            using (var ms = new MemoryStream(Binary))
            {
                var bmp = new Bitmap(ms);
                bmp.Save("C:\\Temp\\temp.png", ImageFormat.Png);
            }

            return new C1InlineUIContainer()
            {
                Content = new System.Windows.Controls.Image()
                {
                    Source = new BitmapImage(
                        new System.Uri("C:\\Temp\\temp.png",
                        System.UriKind.Relative)),
                },
                Height = ImageHeight,
                Width = ImageWidth,
            };
        }
    }
}
