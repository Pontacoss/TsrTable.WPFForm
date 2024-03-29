using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Linq;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{

    public sealed class RtbBullet : C1List, IRtbElement
    {
        public RtbBullet() { }

        public RtbBullet(C1RichTextBox rtb, int index, TextMarkerStyle marker)
        {
            MarkerStyle = marker;

            var count = rtb.Selection.Blocks.Count();
            for (int i = 0; i < count; i++)
            {
                var element = rtb.Selection.Blocks.First(x => x.Index == index);
                rtb.Document.Blocks.Remove(element);
                var item = new RtbBulletItem(element);
                Children.Add(item);
            }
        }

        public ITsrElement GetTsrInstance()
            => new TsrBullet(this);

    }
}
