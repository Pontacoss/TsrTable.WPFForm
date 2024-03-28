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
                var listItem = new C1ListItem();
                listItem.Children.Add(element);
                Children.Add(listItem);
            }
        }

        public ITsrElement ToTsr()
        {
            return new TsrBullet(this);
        }
    }
}
