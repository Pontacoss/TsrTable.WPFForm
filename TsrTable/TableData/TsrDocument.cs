using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;

namespace TsrTable.TableData
{
    public class TsrDocument : ITsrElement
    {
        public Collection<ITsrElement> Children { get; } = new Collection<ITsrElement>();
        public TsrDocument(C1Document document)
        {

        }
        public void ToExcel(C1XLBook book)
        {
            throw new NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new NotImplementedException();
        }

        public C1TextElement ToRtb()
        {
            throw new NotImplementedException();
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }
    }
}
