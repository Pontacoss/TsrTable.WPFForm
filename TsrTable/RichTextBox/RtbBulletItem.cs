using C1.WPF.RichTextBox.Documents;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{

    public sealed class RtbBulletItem : C1ListItem, IRtbElement
    {
        public RtbBulletItem() { }
        public RtbBulletItem(C1Block block)
        {
            Children.Add(block);
        }

        public ITsrElement GetTsrInstance()
            => new TsrBulletItem(this);

    }
}
