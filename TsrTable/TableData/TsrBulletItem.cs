using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Collections.ObjectModel;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public sealed class TsrBulletItem : ITsrElement, ITsrBlock
    {
        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();
        public TsrBulletItem(RtbBulletItem items)
        {
            //foreach (var child in items.Children)
            //{
            //    Children.Add(child.ToTsr());
            //}
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
            => new RtbBulletItem();

    }
}
