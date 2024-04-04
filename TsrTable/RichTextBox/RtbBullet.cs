using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{

    public sealed class RtbBullet : C1List, IRtbElement
    {
        public RtbBullet() { }

        public RtbBullet(C1RichTextBox rtb, TextMarkerStyle marker)
        {
            MarkerStyle = marker;
        }

        public ITsrElement GetTsrInstance()
            => new TsrBullet(this);

    }
}
