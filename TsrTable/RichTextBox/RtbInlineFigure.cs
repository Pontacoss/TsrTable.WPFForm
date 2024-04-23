using C1.WPF.RichTextBox.Documents;
using System.IO;
using System.Windows.Media.Imaging;
using TsrTable.TsrElement;

namespace TsrTable.RichTextBox
{
    internal class RtbInlineFigure : C1InlineUIContainer, IRtbElement
    {
        public byte[] Binary { get; }
        public C1Length ImageHeight { get; }
        public C1Length ImageWidth { get; }

        public RtbInlineFigure() { }

        public RtbInlineFigure(byte[] binary, double height, double width)
        {
            Binary = binary;
            Height = new C1Length(height);
            Width = new C1Length(width);

            using (var ms = new MemoryStream(Binary))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                var img = new System.Windows.Controls.Image();
                img.BeginInit();
                img.Source = bitmapImage;
                img.EndInit();

                Content = img;

            }
        }

        //public override C1TextElement Clone()
        //{
        //    var content = Content as System.Windows.Controls.Image;
        //    return new RtbInlineFigure(Binary, content.ActualHeight, content.ActualWidth);
        //}

        public ITsrElement GetTsrInstance()
        {
            var content = Content as System.Windows.Controls.Image;
            return new TsrInlineFigure(Binary, content.ActualHeight, content.ActualWidth);
        }
    }
}
