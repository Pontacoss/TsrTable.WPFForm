using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Collections.ObjectModel;
using System.Windows.Media;
using TsrTable.TableData;

namespace TsrTable.RichTextBox.TableData
{
    public sealed class TsrPostScript : ITsrElement, ITsrBlock
    {
        public Brush Foreground { get; }

        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();

        public TsrPostScript(Brush color)
        {
            Foreground = color;
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
            => new RtbPostScript(Foreground);

    }
}
