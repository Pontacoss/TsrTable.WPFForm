using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;

namespace TsrTable.TableData
{
    public interface ITsrElement
    {
        C1TextElement ToRtb();
        RtfObject ToWord();
        void ToFlexSheet(C1FlexSheet cfs);
        void ToExcel(C1XLBook book);
    }
}
