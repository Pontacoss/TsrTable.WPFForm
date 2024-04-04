using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public class TsrParameter : ITsrElement
    {
        public string Name { get; }
        public C1TextElement GetRtbInstance() => new RtbParameter(Name);

        public TsrParameter(string name)
        {
            Name = name;
        }
        public void ToExcel(C1XLBook book)
        {
            throw new NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new NotImplementedException();
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }
    }
}
